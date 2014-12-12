using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {


	private GameObject target;
	public float moveSpeed;

	private float aggro = 10.0F;
	public float attackRange = 5.0F;
	private bool attacking = false;
	private float direction = 1;
	private float scaleX;
	private bool StopXAxis = false;
	private bool AttackDelay = false;
	public float tta = 2.5f;
	public GameObject Fireball;
	public float dizzyTime = 2f;
	float dizzy = 0;

	public int initialDizzyFactor = 2;
	public bool animate = false;
	int dizzyFactor;
	public int level = 1;
	// Use this for initialization
	void Start () {
		dizzyFactor = initialDizzyFactor;

		target = GameObject.FindGameObjectWithTag("MyPlayer");
		scaleX = transform.localScale.x;
		moveSpeed = 0.06F;
		GameEventManager.HitEvent += HandleHit;
		transform.FindChild ("power").particleSystem.startSize = (float)(level - 1) / 10.0f;
	}
	
	// Update is called once per frame
	void Update () {

		if (dizzy > 0) {
			dizzy -= Time.deltaTime;
			return;
		}
		if (target == null || target.Equals(null)) {
			return;
		}
		float distance = Vector2.Distance (transform.position, target.transform.position);
		if(distance < aggro)
		{
			enemyMoveMethod();
			if(distance < attackRange) 
			{ 
				StopXAxis = true;
				Attack(); 
			} else {
				StopXAxis = false;
			}
		}
		transform.localScale = new Vector3(direction*scaleX,
		                                   transform.localScale.y,
		                                   transform.localScale.z);
		if(Input.GetKeyDown(KeyCode.F)) {
			Debug.Log ("aggro: "+aggro + " distance: " + distance + "attackRange: " + attackRange);
		}
	}


	void enemyMoveMethod() {
		float posY = transform.position.y;
		float posX = transform.position.x;
		//transform.position += (target.transform.position - transform.position).normalized;

		if(transform.position.y > target.transform.position.y) {
			posY = transform.position.y - (moveSpeed/2);

		}
		else if(transform.position.y < target.transform.position.y) {
			posY = transform.position.y + (moveSpeed/2);

		}

	
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

		if(transform.position.x > target.transform.position.x + 1) {
			direction = 1;
		}
		else if(transform.position.x < target.transform.position.x - 1) {
			direction = -1;
		}

		transform.position = new Vector3(posX, posY, transform.position.z);
	}

	void Attack() 
	{
		if(!AttackDelay) {

			Debug.Log ("Attacking!");

			StartCoroutine(AttackWait());
			if (Fireball != null) {
				if (animate) {
					GetComponentInChildren<Animator>().SetTrigger("hit");

				}
				Destroy(Instantiate(Fireball, transform.position, Quaternion.identity) as GameObject, 5);
			} else {
				GetComponentInChildren<Animator>().SetTrigger("hit");
			}
		}
		
	}
	
	
	//	public void AttackStarted() {
//		attacking = true;
//	}
//	
//	public void AttackEnded() {
//		Debug.Log ("Ending attack");
//		attacking = false;
//	}
	
	public void TriggerHitEvent() {

	//	GameEventManager.TriggerHitEvent(transform.position, direction, 1);
	}

	IEnumerator AttackWait()
	{
		AttackDelay = true;
		yield return new WaitForSeconds(tta);		
		AttackDelay = false;
	}

	void HandleHit(Vector3 pos, float direction, float damage) {
		if (Mathf.Abs (pos.x - transform.position.x) < 2
		    && Mathf.Abs(pos.y - transform.position.y) < 1
		    && ((direction > 0 && transform.position.x > pos.x)
		    || (direction < 0 && transform.position.x < pos.x))) {
			dizzyFactor--;
			rigidbody2D.AddForce(new Vector2(direction*damage*10f, 0), ForceMode2D.Impulse);
			if (dizzyFactor == 0) {
				dizzy = dizzyTime;
				GameObject dizzyObj = Instantiate(Resources.Load<GameObject>("dizzy"), transform.position - new Vector3(0, -2, 0), Quaternion.identity) as GameObject; 
				dizzyObj.transform.parent = transform;
				Destroy(dizzyObj,
					dizzyTime - 0.2f);
				dizzyFactor = initialDizzyFactor;

			}
		}
	}

	void OnDestroy() {
		GameEventManager.HitEvent -= HandleHit;
	}
}
