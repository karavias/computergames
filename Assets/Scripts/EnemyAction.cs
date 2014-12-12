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
		if (player == null || player.Equals(null)) {
			return;
		}


		if (Mathf.Abs(player.transform.position.x - transform.parent.position.x) < distanceX
		    && Mathf.Abs(player.transform.position.y - transform.parent.position.y) < distanceY) {
			player.GetComponent<MyCharacterController>().applyDamage(damage, Mathf.Sign(player.transform.position.x - transform.parent.position.x));
		}
		//	GameEventManager.TriggerHitEvent(transform.position, direction, 1);
	}

	public void TriggerSpecialHit() {
		if (player == null || player.Equals (null)) {
			return;
		}
		Camera.main.SendMessage ("ShakeCamera");
		player.GetComponent<MyCharacterController> ().applyDamage (damage * 2, Mathf.Sign(player.transform.position.x - transform.parent.position.x));
	}
}
