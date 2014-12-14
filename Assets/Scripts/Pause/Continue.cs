using UnityEngine;
using System.Collections;

/**
 * This component is used to resume the game after it was paused.
 **/
public class Continue : MonoBehaviour {

	/**
	 * If the player clicks on this gameobject,
	 * the game is resumed.
	 **/
	void OnMouseDown() {
		Time.timeScale = 1f;
		Destroy (transform.parent.gameObject);
	}
}
