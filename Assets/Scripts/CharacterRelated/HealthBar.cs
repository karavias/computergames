using UnityEngine;
using System.Collections;

/**
 * This class is used to update the health bar indicator
 * of enemy units.
 **/
public class HealthBar : MonoBehaviour {
	//reference to the destroyable component of the unit.
	Destroyable destr;

	//initial X scale of the healthbar
	float initX;

	//reference to the root object of the unit.
	Transform root;

	/**
	 * Initialize variales.
	 **/
	void Start () {
		root = transform.parent.parent.parent;
		destr = root.GetComponent<Destroyable> ();
		initX = transform.localScale.x;

	}
	
	/**
	 * On every frame update the size of the health bar
	 * by changing the scale with the percentage of the 
	 * remaining health of the unit.
	 **/
	void Update () {
		transform.localScale = new Vector3 (
			destr.GetPercent () * initX, transform.localScale.y,
		                                   transform.localScale.z);

		transform.parent.localScale = new Vector3(Mathf.Sign(root.localScale.x) * Mathf.Abs(transform.parent.localScale.x),
		                                                      transform.parent.localScale.y,
		                                                      transform.parent.localScale.z);
		
	}
}
