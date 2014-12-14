using UnityEngine;
using System.Collections;

/**
 * This component is used to fix the 3D Text gameobjects
 * renderer sorting layer and order.
 **/
public class FixTextZ : MonoBehaviour {
	//indicator if it should copy the details from its parent.
	public bool copyParent = false;
	//indicator if it should use an offset for the sorting order parameter.
	public int parentOffset = 0;

	/**
	 * Initialize the sorting layer and order with default values.
	 **/
	void Start () {
		renderer.sortingLayerName= "front";
		renderer.sortingOrder = 3;

	}

	/**
	 * If we should copy the parent values.
	 * Do that on every frame so we are always updated with the parent.
	 **/
	void Update() {
		if (copyParent) {
			renderer.sortingLayerName = transform.parent.renderer.sortingLayerName;
			renderer.sortingOrder = transform.parent.renderer.sortingOrder + parentOffset;
		}
	}

}
