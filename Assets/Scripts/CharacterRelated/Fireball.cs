using UnityEngine;
using System.Collections;
/**
 * This class is used to animate fireballs and arrows.
 **/
public class Fireball : MonoBehaviour {
	//the X position of the fireball (or arrow)
	private float posX;
	//reference to the player's gameobject.
	GameObject target;
	//direction that the fireball is going.
	private float direction;

	/**
	 * Initialize direction, player and position.
	 **/
	void Start () {
		posX = transform.position.x;
		target = GameObject.FindGameObjectWithTag("MyPlayer");
		if (transform.position.x < target.transform.position.x) {
			//shoot right
			direction = 1;
		} else if (transform.position.x > target.transform.position.x) {
			//shoot left
			direction = -1;
		}
		transform.localScale = new Vector3 (-direction * transform.localScale.x,
		                                   transform.localScale.y,
		                                   transform.localScale.z);
	}
	
	/**
	 * On every frame update the X position of the fireball (or arrow)
	 **/
	void Update () {
		if (Time.timeScale == 0) {
			return;
		}
		posX += (direction/10);
		transform.position = new Vector2(posX, transform.position.y);
	}
	


}
