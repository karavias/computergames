using UnityEngine;
using System.Collections;

public class EnemyAction : MonoBehaviour {
	//instance of the player
	GameObject player;

	//strength
	public int damage = 1;

	//Y distance from the unit that can cause damage.
	public float distanceY = 2;

	//X distance from the unit that can cause damage.
	public float distanceX = 2;

	/**
	 * Initialize player
	 **/
	void Start() {
		player = GameObject.FindWithTag ("MyPlayer");
	}

	/**
	 * This function is called by the attack animation of the unit.
	 * 
	 * It checks if the player is in range, and then applies the damage
	 * to the player.
	 **/
	public void TriggerHitEvent() {
		if (player == null || player.Equals(null)) {
			return;
		}

		//apply damage to the player if he is in range.
		if (Mathf.Abs(player.transform.position.x - transform.parent.position.x) < distanceX
		    && Mathf.Abs(player.transform.position.y - transform.parent.position.y) < distanceY) {
			player.GetComponent<MyCharacterController>().applyDamage(damage, Mathf.Sign(player.transform.position.x - transform.parent.position.x));
		}
	}

	/**
	 * This function is called by the special attack animation of the unit.
	 * 
	 * Special attack can cause damage by any distance.
	 * The player can avoid it only if he jumps.
	 **/
	public void TriggerSpecialHit() {
		//check that the player is not dead.
		if (player == null || player.Equals (null)) {
			return;
		}
		//Animate the camera for cool effect.
		Camera.main.SendMessage ("ShakeCamera");
		//apply damage to the player.
		player.GetComponent<MyCharacterController> ().applyDamage (damage * 2, Mathf.Sign(player.transform.position.x - transform.parent.position.x));
	}
}
