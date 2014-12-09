using UnityEngine;
using System.Collections;

public class ZFixer : MonoBehaviour {
	public bool live = false;
	public bool child = false;
	// Use this for initialization
	void Start () {
		if (child) {
			GetComponentInChildren<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);
		} else {
			GetComponent<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);
		}
		if (!live) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (child) {
			GetComponentInChildren<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);
		} else {
			GetComponent<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);
		}
	}
}
