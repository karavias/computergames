using UnityEngine;
using System.Collections;

/**
 * This component is used to start the game level.
 **/
public class StartGame : MonoBehaviour {

	/**
	 * When the user clicks on this gameobject,
	 * start the game level.
	 **/
	void OnMouseDown() {

		Application.LoadLevel ("scene1");
	}
}
