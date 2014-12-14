using UnityEngine;
using System.Collections;

/**
 * This component is used by the camera to follow the player.
 **/
public class CameraFollow : MonoBehaviour {

	//instance of the player's gameobject.
	GameObject player;

	//most left position the camera can go.
	GameObject leftest;

	//most right position the camera can go.
	GameObject rightest;

	//the follow speed of the camera.
	public float followSpeed = 1f;

	//most left position the player can go.
	GameObject leftestChar;

	//reference to the boss's gameobject.
	GameObject boss;

	//indicator if the shake animation should be played.
	bool shake = false;

	//the original Y position of the camera.
	//Used to restore position after shaking.
	float fixedY;

	//the last correct position, without shaking
	//of the camera. Used to correct the position
	//after shake is finished.
	Vector3 lastPos;

	/**
	 * Initialize variables and callbacks.
	 **/
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

	/**
	 * When the player enters the rituals place.
	 * Animate the Camera to a position where the player and the boss
	 * are both visible.
	 **/
	void HandleRoom() {
		iTween.MoveTo (Camera.main.gameObject, iTween.Hash ("x", (boss.transform.position.x + player.transform.position.x)/2, "oncomplete", "UpdateLastPos"));
	}

	/**
	 * After the HandleRoom animation is finished,
	 * be sure to update the lastPos variable.
	 **/
	void UpdateLastPos() {
		lastPos = transform.position;
	}

	/**
	 * On every frame the camera follows the player
	 * only when moving to the right direction.
	 * We also check for shaking and updating the 
	 * most left position the player can go.
	 **/
	void Update () {

		if (player != null && !player.Equals(null)) {
			//if the player is not dead.
			if (player.transform.position.x > transform.position.x) {
				//if the player has moved more right than the camera.
				//Update the position of the camera.
				transform.position = Vector3.Lerp (transform.position,
	                          new Vector3 (player.transform.position.x,
					             fixedY,
					             transform.position.z),
	                          followSpeed * Time.deltaTime);

				//be sure the camera is between its most left and most right limits.
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
			//calculate the most left position for the player.
			//which is the left end of the screen.
			Vector3 screenPos = new Vector3 (0, 0, 10);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPos);
			leftestChar.transform.position = new Vector3 (worldPos.x, 0, 0);
			lastPos = transform.position;
		}

		if (shake) {
			//if we have shake animation, apply a random factor to the cameras potision.
			transform.position = lastPos + new Vector3(Random.Range(-0.1f, 0.1f),
			                                  Random.Range(-0.1f, 0.1f),
			                                  0);
	
		}
		
	}

	/**
	 * This is called when the camera should be shaked.
	 * Is called for the special attack from the boss.
	 **/
	public void ShakeCamera() {
		StartCoroutine(ShakeCameraEnum());
	}

	/**
	 * Enumarator for animating the shake animation 
	 * for 1 second.
	 **/
	IEnumerator ShakeCameraEnum()
	{
		shake = true;
		yield return new WaitForSeconds(1);		
		shake = false;
		transform.position = lastPos;
	}
}
