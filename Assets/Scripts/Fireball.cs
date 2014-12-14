using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	private float posX;
	GameObject target; 
	private float direction;
	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {
		posX += (direction/10);
		transform.position = new Vector2(posX, transform.position.y);


	}
	


}
