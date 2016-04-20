using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	/**
	 * This class is responded for the AI of the enemy units.
	 **/

	//Reference to the player game object.
	private GameObject target;

	//the speed the unit can move.
	public float moveSpeed;

	//the distance from which, if the player appears
	//the unit will try to approach the player and attack.
	private float aggro = 10.0F;

	//The maximum distance from the player that the unit can attack.
	public float attackRange = 5.0F;

	//indicator if the unit currently attacking
	private bool attacking = false;

	//The direction of the player (left, right)
	private float direction = 1;

	//The original X scale of the unit.
	//We use this to rotate the unit left and right.
	private float scaleX;

	//indicator if we should or not move in X axis.
	private bool StopXAxis = false;

	//indicator if the unit can attack or not.
	private bool AttackDelay = false;

	//indicator if the unit can special attack or not.
	bool specialAttackDelay = false;

	//indicator if the unit is the boss.
	public bool boss;

	//indicator if the unit has special attack. Now only the boss has.
	public bool hasSpecialAttack = false;

	//time to wait between normal attacks.
	public float tta = 2.5f;

	//time to wait between special attacks.
	public float ttsa = 7f;

	//if the unit is range unit, then this is the gameobject
	//it throws to the player. Currently it is a fireball, or an arrow.
	public GameObject Fireball;

	//Seconds that the unit is dizzy after hitted many times by the player.
	public float dizzyTime = 2f;

	//indicator of how much time was dizzy.
	float dizzy = 0;

	//indicator of how many hits has to receive to get dizzy.
	public int initialDizzyFactor = 2;

	//indicator of how many hits the unit has to receive
	//more to get dizzy
	int dizzyFactor;

	//indicator if the unit has animated attack move or not.
	public bool animate = false;

	//the level of the unit.
	//Changing the level, changes the strength of the unit.
	public int level = 1;

	//Indicator if the player has been sited by the unit.
	bool targetSpotted = false;

	//If the unit is the boss, this indicates if the boss should be
	//spawned or not. Depending on how fast the user reached the ritual room.
	bool spawn = true;

	//Indicator if the unit has been actually spawned.
	bool spawned = false;

	//The original scaleY of the unit. Is used to animate the spawning for boss units.
	float scaleY;

	/**
	 * Initialize variables.
	 **/
	void Start () {
		scaleY = transform.localScale.y;
		dizzyFactor = initialDizzyFactor;
		target = GameObject.FindGameObjectWithTag("MyPlayer");
		scaleX = transform.localScale.x;
		moveSpeed = 0.06F;
		GameEventManager.HitEvent += HandleHit;
		transform.FindChild ("power").GetComponent<ParticleSystem>().startSize = (float)(level - 1) / 10.0f;
		if (boss) {
			GameEventManager.EnteredRitualRoom += PlayerEnteredRoom;
			GetComponent<Collider2D>().enabled = false;
			transform.localScale = new Vector3(transform.localScale.x,
			                                   0,
			                                   transform.localScale.z);
		}
	}

	/**
	 * Callback function that is called when the user enteres the ritual place.
	 * If the user reached fast enough, the boss will not spawened.
	 * Otherwise it will be spawned after in after 30 seconds.
	 **/
	void PlayerEnteredRoom() {
		if (Timer.remainingTime > 0) {
			spawn = false;
			Destroy(gameObject);
		} else {
			spawn = true;
			iTween.ScaleTo(gameObject, iTween.Hash("y", scaleY, "time", 30f, "easyType", iTween.EaseType.linear,
			                                       "oncomplete", "HandleSpawnComplete"));

		}
		GameEventManager.EnteredRitualRoom -= PlayerEnteredRoom;
	}

	/**
	 * If the boss is spawned, this is a callback function that is called
	 * when the spawning is completed.
	 **/
	public void HandleSpawnComplete() {
		if (spawn) {
			spawned = true;
			GetComponent<Collider2D>().enabled = true;
		}
	}

	/**
	 * Calculate the new position of the unit
	 * and check if the unit should attack.
	 **/
	void Update () {
		if (Time.timeScale == 0) {
			return;
		}
		//if is boss but not spawned. Do nothing.
		if (boss && !spawned) {
			return;
		}
		//if is dizzy. Do nothing.
		if (dizzy > 0) {
			dizzy -= Time.deltaTime;
			return;
		}
		//if the player is dead. Do nothing.
		if (target == null || target.Equals(null)) {
			return;
		}
		//check the distance of the unit with the player.
		float distance = Vector2.Distance (transform.position, target.transform.position);
		//if the unit is on site.
		if(targetSpotted || distance < aggro)
		{
			targetSpotted = true;
			//calculate next move position.
			enemyMoveMethod();
			//check if should attack.
			if(distance < attackRange) 
			{ 
				StopXAxis = true;
				Attack(); 
			} else {
				StopXAxis = false;
			}
			//check if should special attack.
			if (hasSpecialAttack) {
				SpecialAttack();
			}
		}
		//rotate the unit using the X scale factor according
		//to its direction.
		transform.localScale = new Vector3(direction*scaleX,
		                                   transform.localScale.y,
		                                   transform.localScale.z);

	}

	/**
	 * This method caltulates the next
	 * position of the unit.
	 **/
	void enemyMoveMethod() {
		float posY = transform.position.y;
		float posX = transform.position.x;
		//calculate the unit new the y axis.
		if(transform.position.y > target.transform.position.y) {
			posY = transform.position.y - (moveSpeed/2);

		}
		else if(transform.position.y < target.transform.position.y) {
			posY = transform.position.y + (moveSpeed/2);

		}

		//calculate the unit new x axis.
		if(transform.position.x > target.transform.position.x + 1) {
			if(!StopXAxis) 
				posX = transform.position.x - moveSpeed;
			direction = 1;
		}
		else if(transform.position.x < target.transform.position.x - 1) {
			if(!StopXAxis) 
				posX = transform.position.x + moveSpeed;
			direction = -1;
		}
		//update the position.
		transform.position = new Vector3(posX, posY, transform.position.z);
	}

	/**
	 * This method handles the attack feature
	 * of the unit.
	 **/
	void Attack() 
	{
		//if not preparing to attack.
		if(!AttackDelay) {
			//reinitialize the preparation.
			StartCoroutine(AttackWait());
			//if the unit is range unit, then it will throw a gameobject
			//to the player.
			if (Fireball != null) {
				if (animate) {
					GetComponentInChildren<Animator>().SetTrigger("hit");

				}
				Destroy(Instantiate(Fireball, transform.position, Quaternion.identity) as GameObject, 5);
			} else {
				//else it will animate a hit animation.
				GetComponentInChildren<Animator>().SetTrigger("hit");
			}
		}
		
	}

	/**
	 * This method handles the special attack
	 * feature of the unit.
	 **/
	void SpecialAttack() {
		//if the unit is prepared to attack.
		if(!specialAttackDelay) {
			//reinitialize the preparation.
			StartCoroutine(SpecialAttackWait());
			//attack.
			GetComponentInChildren<Animator>().SetTrigger("hitSpecial");

		}
	}


	/**
	 * This enumarator is used
	 * to initialize the waiting time between the attacks.
	 **/
	IEnumerator AttackWait()
	{
		AttackDelay = true;
		yield return new WaitForSeconds(tta);		
		AttackDelay = false;
	}

	/**
	 * This enumarator is used
	 * to initialize the waiting time between special attacks.
	 **/
	IEnumerator SpecialAttackWait()
	{
		specialAttackDelay = true;
		yield return new WaitForSeconds(ttsa);		
		specialAttackDelay = false;
	}

	/**
	 * When the player attacks. This is the callback function
	 * the unit uses to check if it gets damate.
	 **/
	void HandleHit(Vector3 pos, float direction, float damage) {
		//if the unit is in the attack range of the player
		//and in the correct direction.
		if (Mathf.Abs (pos.x - transform.position.x) < 2
		    && Mathf.Abs(pos.y - transform.position.y) < 1
		    && ((direction > 0 && transform.position.x > pos.x)
		    || (direction < 0 && transform.position.x < pos.x))) {
			//reduce the number of hits to get dizzy.
			dizzyFactor--;
			//throw the unit back from the hit.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(direction*damage*10f, 0), ForceMode2D.Impulse);
			//if the unit received enough hits to get dizzy.
			//initialize dizzyness
			if (dizzyFactor == 0) {
				dizzy = dizzyTime;
				//animate birds over the head of the unit.
				GameObject dizzyObj = Instantiate(Resources.Load<GameObject>("dizzy"), transform.position - new Vector3(0, -2, 0), Quaternion.identity) as GameObject; 
				dizzyObj.transform.parent = transform;
				Destroy(dizzyObj,
					dizzyTime - 0.2f);
				dizzyFactor = initialDizzyFactor;

			}
		}
	}


	/**
	 * When the unit is destroyed.
	 * We remove the HitEvent handler
	 * so the callback stops getting called on every hit.
	 **/
	void OnDestroy() {
		GameEventManager.HitEvent -= HandleHit;
	}
}
