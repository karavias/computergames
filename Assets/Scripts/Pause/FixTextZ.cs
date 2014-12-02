using UnityEngine;
using System.Collections;

public class FixTextZ : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.sortingLayerName= "front";
		renderer.sortingOrder = 2;

	}
	

}
