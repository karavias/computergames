using UnityEngine;
using System.Collections;

public class ExitToMenu : MonoBehaviour {

	void OnMouseDown() {
		Application.LoadLevel ("MainMenu");
	}
}
