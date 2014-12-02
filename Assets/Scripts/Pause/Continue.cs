using UnityEngine;
using System.Collections;

public class Continue : MonoBehaviour {

	void OnMouseDown() {
		Time.timeScale = 1f;
		Destroy (transform.parent.gameObject);
	}
}
