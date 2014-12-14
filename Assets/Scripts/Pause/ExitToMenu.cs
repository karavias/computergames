using UnityEngine;
using System.Collections;

/**
 * This component is used to exit from the level to main menu.
 **/
public class ExitToMenu : MonoBehaviour {

	/**
	 * When the player clicks on this gameobject
	 * we load the main menu scene.
	 **/
	void OnMouseDown() {
		Time.timeScale = 1f;
		Application.LoadLevel ("MainMenu");
	}
}
