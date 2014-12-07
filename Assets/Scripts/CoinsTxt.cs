using UnityEngine;
using System.Collections;

public class CoinsTxt : MonoBehaviour {

	TextMesh txtMesh;

	void Start() {
		txtMesh = GetComponent<TextMesh> ();
	}

	void Update() {
		txtMesh.text = "Coins: " + MyCharacterController.coins;
	}
}
