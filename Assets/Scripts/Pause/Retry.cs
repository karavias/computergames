using UnityEngine;
using System.Collections;

/**
 * This component is used when the player losed the level
 * and wants to play again.
 **/
public class Retry : MonoBehaviour {

	/**
	 * When the player cliks on this gameobject the level is loaded again.
	 **/
	void OnMouseDown() {
		Application.LoadLevel (Application.loadedLevelName);
	}
}
