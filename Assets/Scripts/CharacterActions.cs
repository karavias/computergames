using UnityEngine;
using System.Collections;

public class CharacterActions : MonoBehaviour {

	public void TriggerHitEvent() {
		GameEventManager.TriggerHitEvent(transform.position, 
		                                 transform.parent.GetComponent<MyCharacterController>().direction, 1);
	}
}
