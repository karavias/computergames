using UnityEngine;
using System.Collections;

/**
 * This component gives a custom sorging layer and order
 * to the unit.
 **/
public class ZCustomFixer : MonoBehaviour {
	//the custom layer name.
	public string layerName;
	//the custom layer order.
	public int layerOrder;

	/**
	 * Initialize the renderer with the layer name and order.
	 **/
	void Start () {
		GetComponent<Renderer>().sortingLayerName = layerName;
		GetComponent<Renderer>().sortingOrder = layerOrder;
	}
	
}
