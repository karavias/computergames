using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	GameObject player;
	GameObject leftest;
	GameObject rightest;
	public float followSpeed = 1f;
	GameObject leftestChar;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MyPlayer");
		leftest = GameObject.Find ("Leftest");
		rightest = GameObject.Find ("Rightest");
		leftestChar = GameObject.Find ("LeftestCharacter");
	}
	
	// Update is called once per frame
	void Update () {

		if (player == null || player.Equals(null)) {
			return;
		}
		if (player.transform.position.x > transform.position.x) {
			transform.position = Vector3.Lerp (transform.position,
                          new Vector3 (player.transform.position.x,
				             transform.position.y,
				             transform.position.z),
                          followSpeed * Time.deltaTime);

			if (leftest.transform.position.x > transform.position.x) {
					transform.position = new Vector3 (leftest.transform.position.x,
                                 transform.position.y,
                                 transform.position.z);
			}
			if (rightest.transform.position.x < transform.position.x) {
					transform.position = new Vector3 (rightest.transform.position.x,
                                 transform.position.y,
                                 transform.position.z);
			}
			Vector3 screenPos = new Vector3 (0, 0, 10);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPos);
			leftestChar.transform.position = new Vector3 (worldPos.x, 0, 0);
		}

	}
}
