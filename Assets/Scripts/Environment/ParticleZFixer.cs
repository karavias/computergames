using UnityEngine;
using System.Collections;

/**
 * This component is used to update the sprites renderer
 * with the sorting layer and order of its parent.
 * So it is displayed correctly in the pseydo 3D world.
 **/
public class ParticleZFixer : MonoBehaviour {


	
	/**
	 * On every frame, update the sorting layer and order of the particle system
	 * with the one the parent has.
	 **/
	void Update () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = transform.parent.GetComponent<Renderer>().sortingLayerName;
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = transform.parent.GetComponent<Renderer>().sortingOrder;
	}
}
