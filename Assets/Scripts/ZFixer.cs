using UnityEngine;
using System.Collections;

public class ZFixer : MonoBehaviour {
	public bool live = false;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);
		if (!live) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);

	}
}
