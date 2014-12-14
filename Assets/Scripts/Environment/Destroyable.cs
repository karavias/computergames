using UnityEngine;
using System.Collections;

/**
 * This class is used in all objects and enemy units
 * that can receive hit from the player.
 **/
public class Destroyable : MonoBehaviour {

	//the maximum number of hits that the unit can receive.
	public float health = 5;
	//indicator of the initial maximum health of the unit. Which is the
	//initial value of the health variable.
	float initialMax;
	//indicator if the unit should throw food when gets destroyed.
	public bool throwItem = false;

	/**
	 * Initialize variables.
	 **/
	void Start() {
		EnemyMove enMove = GetComponent<EnemyMove> ();
		if (enMove != null) {
			//if it is an enemy unit and has level.
			//then update the initial health with the health*level.
			health = health * enMove.level;
		}
		GameEventManager.HitEvent += HandleHit;
		initialMax = health;
	}

	/**
	 * This function is called everytime the player throws a hit.
	 * If the unit/object is in the range of the players hit
	 * then received damage.
	 **/
	void HandleHit(Vector3 pos, float direction, float damage) {
		//Check that the item is in the range of the player
		//and in the right direction.
		if (Mathf.Abs (pos.x - transform.position.x) < 2
		    && Mathf.Abs(pos.y - transform.position.y) < 1
			&& ((direction > 0 && transform.position.x > pos.x)
		    || (direction < 0 && transform.position.x < pos.x))) {
			//reduce health.
			health -= damage;
			//animate a cartoon "pow" effect.
			Destroy(Instantiate(Resources.Load<GameObject>("pow"), 
			                    transform.position +
			                    new Vector3(Random.Range(-0.2f, 0.2f),
			            					0.5f + Random.Range(-0.1f, 0.2f),
			            					0)
			                    , Quaternion.identity), 0.4f);
			//if there is no more health.
			if (health <= 0) {
				//if the object should throw food.
				//Throw a random food with value 1, 2, 3 or a coin.
				if (throwItem) {
					int roll = Random.Range(0, 100);
					if (roll < 30) {
						Instantiate(Resources.Load<GameObject>("1"), transform.position, Quaternion.identity);
					} else if (roll < 50) {
						Instantiate(Resources.Load<GameObject>("2"), transform.position, Quaternion.identity);
					} else if (roll < 60) {
						Instantiate(Resources.Load<GameObject>("3"), transform.position, Quaternion.identity);
					} else if (roll < 70) {
						Instantiate(Resources.Load<GameObject>("coin"), transform.position, Quaternion.identity);
					}
				} else {
					//if the unit is not throwing an item
					//it still has 10% chance to throw a coin.
					int roll = Random.Range(0, 100);
					if (roll < 10) {
						Instantiate(Resources.Load<GameObject>("coin"), transform.position, Quaternion.identity);
					} 
				}
				//finally destroy the damaged gameobject.
				Destroy(gameObject);
			}
		}
	}

	void OnDestroy() {
		GameEventManager.HitEvent -= HandleHit;
	}

	public float GetPercent() {
		return health / initialMax;
	}


}
