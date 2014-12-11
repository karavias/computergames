using UnityEngine;
using System.Collections;

public class LeftestCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = new Vector3 (0, 0, 10);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPos);
		transform.position = new Vector3 (worldPos.x, 0, 0);
	}
}
