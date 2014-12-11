using UnityEngine;
using System.Collections;

public class ParticleZFixer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		particleSystem.renderer.sortingLayerName = transform.parent.renderer.sortingLayerName;
		particleSystem.renderer.sortingOrder = transform.parent.renderer.sortingOrder;
	}
}
