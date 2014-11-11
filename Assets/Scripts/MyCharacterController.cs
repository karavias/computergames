using UnityEngine;
using System.Collections;

public class MyCharacterController : MonoBehaviour {
	Animator animator;
	float scaleX;
	public float jumpForce = 10f;
	public float speed = 1f;
	public float verticalSpeed = 1f;
	float direction = 1;
	float preJumpY;
	float jumpTime;
	bool jumping = false;
	float curJumpTime = 1f;
	GameObject shadow;
	public float shadowOffset = -1;
	// Use this for initialization
	void Start () {
		shadow = GameObject.Find ("shadow");
		scaleX = transform.localScale.x;
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetBool ("walk", Input.GetAxis ("Horizontal") != 0
		                  || Input.GetAxis("Vertical") != 0);
		float sign;
		if (Input.GetAxis("Horizontal") < 0) {
			direction = -1;
		} else if (Input.GetAxis("Horizontal") > 0) {
			direction = 1;
		}
		transform.localScale = new Vector3(direction*scaleX,
		                                   transform.localScale.y,
		                                   transform.localScale.z);
		if (jumping) {
			preJumpY += Input.GetAxis ("Vertical") * verticalSpeed * Time.deltaTime;
			transform.position = 
				new Vector3(
					transform.position.x + Input.GetAxis ("Horizontal") * speed * Time.deltaTime,
					preJumpY + jumpForce * Mathf.Sin(2 * Mathf.PI * (Time.time - jumpTime) / (curJumpTime * 2)),
					transform.position.y
					);
			if (Time.time - jumpTime >= curJumpTime) {
				jumping = false;
			}


		} else {
			transform.position += new Vector3 (Input.GetAxis ("Horizontal")* speed * Time.deltaTime, 
			                                   Input.GetAxis ("Vertical") * verticalSpeed * Time.deltaTime
			                                   , 0); 

		}
		collider2D.enabled = !jumping;
		if (jumping) {
			shadow.transform.position = new Vector3 (transform.position.x, preJumpY + shadowOffset, -1);
			GetComponent<SpriteRenderer> ().sortingOrder = 1000 - (int)(preJumpY * 100);

		} else {
			shadow.transform.position = new Vector3 (transform.position.x, transform.position.y + shadowOffset, -1);
			GetComponent<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);

		}
		if (Input.GetKeyDown(KeyCode.X)) {
			animator.SetTrigger("attack");
		}
		if (Input.GetKeyDown(KeyCode.Z) && !jumping) {
			animator.SetTrigger("jump");
			preJumpY = transform.position.y;
			jumpTime = Time.time;
			jumping = true;
			//rigidbody.AddForce(new Vector3(0, jumpForce, 0));
		}
	}
}
