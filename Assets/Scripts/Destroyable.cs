﻿using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {

	public float health = 5;
	Animator anim;
	void Start() {
		anim = GetComponent<Animator> ();
		GameEventManager.HitEvent += HandleHit;
	}

	void HandleHit(Vector3 pos, float direction, float damage) {
		Debug.Log ("Looking");
		if (Vector3.Distance(pos, transform.position) < 2
		    && ((direction > 0 && transform.position.x > pos.x)
		    || (direction < 0 && transform.position.x < pos.x))) {
			Debug.Log("received a hit");
			health -= damage;
			//anim.SetTrigger("Hitted");
			if (health <= 0) {
				GameEventManager.HitEvent -= HandleHit;
				Destroy(gameObject);
				//todo: play animation for destroying,
				//and then destroy after 2-3 seconds.
			}
		}
	}


}