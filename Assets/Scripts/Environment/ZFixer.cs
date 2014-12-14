using UnityEngine;
using System.Collections;
/**
 * This component creates the pseudo 3D effect in the 2D world
 * by updating the renderer's sorting order according to the y position
 * of the unit/object/item.
 **/
public class ZFixer : MonoBehaviour {
	//indicator if the unit is a moveable unit.
	//So we should update the sorting order on every frame
	//since it changes while the y position of the unit change.
	public bool live = false;
	//Indicator if we should update the children of the gameobject
	//Is used for empty gameobjects that contain sprite children.
	public bool child = false;

	//indicator if it is a unit that has "power" particle animation
	//so we should update the particle's sorting order as well.
	public GameObject power;
	// Use this for initialization
	void Start () {
		//if we look for children
		if (child) {
			//Update the sorting order in all children.
			foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>()) {
				rend.sortingOrder = 998 - (int)(transform.position.y * 100);	
			}		
		} else {
			//else just update the sorting order of the current gameobject.
			GetComponent<SpriteRenderer> ().sortingOrder = 998 - (int)(transform.position.y * 100);
		}
		if (power != null) {
			//if the gameobject has a power particle system.
			//update also the particle system.
			power.particleSystem.renderer.sortingOrder = 1001 - (int)(transform.position.y * 100);
		}
		//if we don't want live update. Disable component.
		if (!live) {
			enabled = false;
		}
	}
	
	/**
	 * for live updates. We update the sorting order on every frame.
	 **/
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
