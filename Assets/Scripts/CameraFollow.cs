using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	GameObject player;
	GameObject leftest;
	GameObject rightest;
	public float followSpeed = 1f;
	GameObject leftestChar;
	GameObject boss;
	bool shake = false;
	float fixedY;
	Vector3 lastPos;
	// Use this for initialization
	void Start () {
		fixedY = transform.position.y;
		shake = false;
		boss = GameObject.FindWithTag ("boss");
		player = GameObject.FindWithTag ("MyPlayer");
		leftest = GameObject.Find ("Leftest");
		rightest = GameObject.Find ("Rightest");
		leftestChar = GameObject.Find ("LeftestCharacter");
		GameEventManager.EnteredRitualRoom += HandleRoom;
		lastPos = transform.position;
	}

	void HandleRoom() {
		iTween.MoveTo (Camera.main.gameObject, iTween.Hash ("x", (boss.transform.position.x + player.transform.position.x)/2, "oncomplete", "UpdateLastPos"));
	}

	void UpdateLastPos() {
		lastPos = transform.position;
	}
	// Update is called once per frame
	void Update () {

		if (player != null && !player.Equals(null)) {
			if (player.transform.position.x > transform.position.x) {
				transform.position = Vector3.Lerp (transform.position,
	                          new Vector3 (player.transform.position.x,
					             fixedY,
					             transform.position.z),
	                          followSpeed * Time.deltaTime);

				if (leftest.transform.position.x > transform.position.x) {
						transform.position = new Vector3 (leftest.transform.position.x,
					                                  fixedY,
	                                 transform.position.z);
				}
				if (rightest.transform.position.x < transform.position.x) {
						transform.position = new Vector3 (rightest.transform.position.x,
					                                  fixedY,
	                                 transform.position.z);
				}

			}
			Vector3 screenPos = new Vector3 (0, 0, 10);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPos);
			leftestChar.transform.position = new Vector3 (worldPos.x, 0, 0);
			lastPos = transform.position;
		}
		Debug.Log ("campos? " + lastPos);
		if (shake) {
			transform.position = lastPos + new Vector3(Random.Range(-0.1f, 0.1f),
			                                  Random.Range(-0.1f, 0.1f),
			                                  0);
	
		}
		
	}

	public void ShakeCamera() {
		StartCoroutine(ShakeCameraEnum());
	}

	IEnumerator ShakeCameraEnum()
	{
		shake = true;
		yield return new WaitForSeconds(1);		
		shake = false;
		transform.position = lastPos;
	}
}
