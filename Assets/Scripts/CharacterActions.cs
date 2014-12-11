using UnityEngine;
using System.Collections;

public class CharacterActions : MonoBehaviour {

	public void TriggerHitEvent() {
		GameEventManager.TriggerHitEvent(transform.parent.parent.position, 
                         transform.parent.parent.GetComponent<MyCharacterController>().direction,
		                                 Upgrades.damage);
	}
}
