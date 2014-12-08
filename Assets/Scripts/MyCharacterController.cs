using UnityEngine;
using System.Collections;

public class MyCharacterController : MonoBehaviour {
//	- Timer (+ score) 
//	- Lives
//	- Player Dies 
//	- Ritual being disrupted
//	- Ritual summons boss
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
	bool attacking = false;
	GameObject topest;
	GameObject bottomest;
	GameObject rightest;
	GameObject leftest;

	public static int coins = 0;

	void Awake() {
		GameEventManager.ClearAll ();
	}

	// Use this for initialization
	void Start () {
		shadow = GameObject.Find ("shadow");
		scaleX = transform.localScale.x;
		animator = GetComponent<Animator> ();
		topest = GameObject.Find ("Topest");
		bottomest = GameObject.Find ("Bottomest");
		rightest = GameObject.Find ("RightestCharacter");
		leftest = GameObject.Find ("LeftestCharacter");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Instantiate(Resources.Load<GameObject>("pause"),
			            new Vector3(Camera.main.transform.position.x,
			            			Camera.main.transform.position.y,
			            		0)
			            ,
			            Quaternion.identity);
			Time.timeScale = 0;
		}
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
					0
					);
			if (Time.time - jumpTime >= curJumpTime) {
				jumping = false;
			}


		} else {
			transform.position += new Vector3 (Input.GetAxis ("Horizontal")* speed * Time.deltaTime, 
			                                   Input.GetAxis ("Vertical") * verticalSpeed * Time.deltaTime
			                                   , 0); 
			if (transform.position.y > topest.transform.position.y) {
				transform.position = new Vector3(transform.position.x,
				                                 topest.transform.position.y,
				                                 transform.position.z);
			}
			if (transform.position.y < bottomest.transform.position.y) {
				transform.position = new Vector3(transform.position.x,
				                                 bottomest.transform.position.y,
				                                 transform.position.z);
			}
			if (transform.position.x < leftest.transform.position.x) {
				transform.position = new Vector3(leftest.transform.position.x,
				                                 transform.position.y,
				                                 transform.position.z);
			}
			if (transform.position.x > rightest.transform.position.x) {
				transform.position = new Vector3(rightest.transform.position.x,
				                                 transform.position.y,
				                                 transform.position.z);
			}
		}
		collider2D.enabled = !jumping;
		if (jumping) {
			shadow.transform.position = new Vector3 (transform.position.x, preJumpY + shadowOffset, -1);
			GetComponent<SpriteRenderer> ().sortingOrder = 1000 - (int)(preJumpY * 100);

		} else {
			shadow.transform.position = new Vector3 (transform.position.x, transform.position.y + shadowOffset, -1);
			GetComponent<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);

		}
		if (Input.GetKeyDown(KeyCode.X)
		    && !jumping && !attacking) {
			animator.SetTrigger("attack");
		}
		if (Input.GetKeyDown(KeyCode.Z) && !jumping) {
			//animator.SetTrigger("jump");
			preJumpY = transform.position.y;
			jumpTime = Time.time;
			jumping = true;
			//rigidbody.AddForce(new Vector3(0, jumpForce, 0));
		}
	}

	public void AttackStarted() {
		attacking = true;
	}

	public void AttackEnded() {
		Debug.Log ("Ending attack");
		attacking = false;
	}

	public void TriggerHitEvent() {
		Debug.Log (transform.position + " --- " + direction);
		GameEventManager.TriggerHitEvent(transform.position, direction, 1);
	}




	public void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "RitualRoom") {
			GameEventManager.TriggerEnteredRitualRoom();
		} else if (col.gameObject.tag == "coin") {
			coins ++;
			Destroy(col.gameObject);
		}
		if (col.gameObject.tag == "Fireball") {
			Debug.Log ("Iz burning. Oh my gawd!!");
		}
	}
}
