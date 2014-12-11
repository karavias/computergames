﻿using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {

	public float health = 5;
	Animator anim;
	float initialMax;
	public bool throwItem = false;
	void Start() {
		anim = GetComponent<Animator> ();
		GameEventManager.HitEvent += HandleHit;
		initialMax = health;
	}

	void HandleHit(Vector3 pos, float direction, float damage) {
		if (Mathf.Abs (pos.x - transform.position.x) < 2
		    && Mathf.Abs(pos.y - transform.position.y) < 1
			&& ((direction > 0 && transform.position.x > pos.x)
		    || (direction < 0 && transform.position.x < pos.x))) {
			Debug.Log("received a hit");
			health -= damage;
			Destroy(Instantiate(Resources.Load<GameObject>("pow"), 
			                    transform.position +
			                    new Vector3(Random.Range(-0.2f, 0.2f),
			            					0.5f + Random.Range(-0.1f, 0.2f),
			            					0)
			                    , Quaternion.identity), 0.4f);
			if (health <= 0) {
				GameEventManager.HitEvent -= HandleHit;
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
					int roll = Random.Range(0, 100);
					if (roll < 10) {
						Instantiate(Resources.Load<GameObject>("coin"), transform.position, Quaternion.identity);
					} 
				}
				Destroy(gameObject);
				//todo: play animation for destroying,
				//and then destroy after 2-3 seconds.
			}
		}
	}

	public float GetPercent() {
		return health / initialMax;
	}


}
