using UnityEngine;
using System.Collections;

public class ZFixer : MonoBehaviour {
	public bool live = false;
	public bool child = false;
	public GameObject power;
	// Use this for initialization
	void Start () {
		if (child) {
			
			foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>()) {
				rend.sortingOrder = 998 - (int)(transform.position.y * 100);	
			}		
		} else {
			GetComponent<SpriteRenderer> ().sortingOrder = 998 - (int)(transform.position.y * 100);
		}
		if (power != null) {
			power.particleSystem.renderer.sortingOrder = 1001 - (int)(transform.position.y * 100);
		}
		if (!live) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (child) {

			foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>()) {
				rend.sortingOrder = 998 - (int)(transform.position.y * 100);	
			}
		} else {

			GetComponent<SpriteRenderer> ().sortingOrder = 998 - (int)(transform.position.y * 100);
		}
		if (power != null) {
			power.particleSystem.renderer.sortingOrder = 1001 - (int)(transform.position.y * 100);
		}
	}
}
