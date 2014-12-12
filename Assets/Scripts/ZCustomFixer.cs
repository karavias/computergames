using UnityEngine;
using System.Collections;

public class ZCustomFixer : MonoBehaviour {

	public string layerName;
	public int layerOrder;
	// Use this for initialization
	void Start () {
		renderer.sortingLayerName = layerName;
		renderer.sortingOrder = layerOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
