using UnityEngine;
using System.Collections;

public class EnemyAction : MonoBehaviour {
	GameObject player;
	public int damage = 1;
	public float distanceY = 2;
	public float distanceX = 2;
	void Start() {
		player = GameObject.FindWithTag ("MyPlayer");
	}

	public void TriggerHitEvent() {
		Debug.Log ("Attacking triggering");
		if (player == null || player.Equals(null)) {
			return;
		}
		Debug.Log ("Attacking triggering player:: " + (Mathf.Abs(player.transform.position.x - transform.parent.position.x) < distanceX) + " -- " +
		           (Mathf.Abs(player.transform.position.y - transform.parent.position.y) < distanceY));

		if (Mathf.Abs(player.transform.position.x - transform.parent.position.x) < distanceX
		    && Mathf.Abs(player.transform.position.y - transform.parent.position.y) < distanceY) {
			player.GetComponent<MyCharacterController>().applyDamage(damage);
		}
		//	GameEventManager.TriggerHitEvent(transform.position, direction, 1);
	}
}
