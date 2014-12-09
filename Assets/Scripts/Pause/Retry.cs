using UnityEngine;
using System.Collections;

public class Retry : MonoBehaviour {

	void OnMouseDown() {
		Application.LoadLevel (Application.loadedLevelName);
	}
}
