using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	GameObject player;
	GameObject leftest;
	GameObject rightest;
	public float followSpeed = 1f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MyPlayer");
		leftest = GameObject.Find ("Leftest");
		rightest = GameObject.Find ("Rightest");
	}
	
	// Update is called once per frame
	void Update () {


		transform.position = Vector3.Lerp (transform.position,
		                                  new Vector3 (player.transform.position.x,
							             transform.position.y,
							             transform.position.z),
		                                  followSpeed * Time.deltaTime);

		if (leftest.transform.position.x > transform.position.x) {
			transform.position = new Vector3(leftest.transform.position.x,
			                                 transform.position.y,
			                                 transform.position.z);
		}
		if (rightest.transform.position.x < transform.position.x) {
			transform.position = new Vector3(rightest.transform.position.x,
			                                 transform.position.y,
			                                 transform.position.z);
		}
	}
}
