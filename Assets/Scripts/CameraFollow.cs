using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MyPlayer");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (player.transform.position + " - " + player.name);
		transform.position = new Vector3 (player.transform.position.x,
		                                  transform.position.y,
		                                  transform.position.z);
	}
}
