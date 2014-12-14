using UnityEngine;
using System.Collections;

public class CharacterActions : MonoBehaviour {

	/**
	 * This method is called by the attack animation of the character when we
	 * should check if the character hit enemies.
	 **/
	public void TriggerHitEvent() {
		//Trigger an attack event with the characters position, direction and damage power.
		GameEventManager.TriggerHitEvent(transform.parent.parent.position, 
                         transform.parent.parent.GetComponent<MyCharacterController>().direction,
		                                 Upgrades.damage);
	}
}
