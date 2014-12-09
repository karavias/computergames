using UnityEngine;
using System.Collections;

public class EnemyAction : MonoBehaviour {
	GameObject player;
	void Start() {
		player = GameObject.FindWithTag ("MyPlayer");
	}

	public void TriggerHitEvent() {
		if (Mathf.Abs(player.transform.position.x - transform.parent.position.x) < 2
		    && Mathf.Abs(player.transform.position.y - transform.parent.position.y) < 2) {
			player.GetComponent<MyCharacterController>().applyDamage(1);
		}
		//	GameEventManager.TriggerHitEvent(transform.position, direction, 1);
	}
}
