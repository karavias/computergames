using UnityEngine;
using System.Collections;

/**
 * This component is used to quit the game.
 **/
public class ExitGame : MonoBehaviour {

	/**
	 * When the user clicks this object,
	 * exit the game.
	 **/
	void OnMouseDown() {
		Application.Quit ();
	}
}
