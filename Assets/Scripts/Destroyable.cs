using UnityEngine;
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
		if (Mathf.Abs (pos.x - transform.position.x) < 2
		    && Mathf.Abs(pos.y - transform.position.y) < 1
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
