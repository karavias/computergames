using UnityEngine;
using System.Collections;

public class ExitToMenu : MonoBehaviour {

	void OnMouseDown() {
		Time.timeScale = 1f;
		Application.LoadLevel ("MainMenu");
	}
}
