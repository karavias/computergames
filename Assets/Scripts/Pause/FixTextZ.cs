using UnityEngine;
using System.Collections;

public class FixTextZ : MonoBehaviour {

	public bool copyParent = false;

	// Use this for initialization
	void Start () {
		renderer.sortingLayerName= "front";
		renderer.sortingOrder = 3;

	}

	void Update() {
		if (copyParent) {
			renderer.sortingLayerName = transform.parent.renderer.sortingLayerName;
			renderer.sortingOrder = transform.parent.renderer.sortingOrder;
		}
	}

}
