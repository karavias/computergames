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
	public float direction = 1;
	float preJumpY;
	float jumpTime;
	bool jumping = false;
	float curJumpTime = 1f;
	bool attacking = false;
	GameObject topest;
	GameObject bottomest;
	GameObject rightest;
	GameObject leftest;
	public float maxHealth;
	public float health;
	public static int coins = 0;
	Transform healthIndicator;
	float healthIndicatorX;

	void Awake() {
		GameEventManager.ClearAll ();
	}

	// Use this for initialization
	void Start () {
		health = maxHealth;
		scaleX = transform.localScale.x;
		animator = GetComponentInChildren<Animator> ();
		topest = GameObject.Find ("Topest");
		bottomest = GameObject.Find ("Bottomest");
		rightest = GameObject.Find ("RightestCharacter");
		leftest = GameObject.Find ("LeftestCharacter");
		healthIndicator = transform.FindChild("healthbar").FindChild ("indicator");
		healthIndicatorX = healthIndicator.localScale.x;

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
			GetComponentInChildren<SpriteRenderer> ().sortingOrder = 1000 - (int)(preJumpY * 100);

		} else {
			GetComponentInChildren<SpriteRenderer> ().sortingOrder = 1000 - (int)(transform.position.y * 100);

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
		healthIndicator.localScale = new Vector3(
			(health/maxHealth) * healthIndicatorX,
			healthIndicator.localScale.y,
			healthIndicator.localScale.z
			);
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
			applyDamage(1);
		}
		if (col.gameObject.tag == "healthup") {
			applyDamage(-1);
			Destroy(col.gameObject);
		}
	}

	public void applyDamage(int damage) {
		health -= damage;
		if (damage > 0) {
			Destroy(Instantiate(Resources.Load<GameObject>("pow"), 
			                    transform.position +
			                    new Vector3(Random.Range(-0.2f, 0.2f),
			            0.5f + Random.Range(-0.1f, 0.2f),
			            0)
			                    , Quaternion.identity), 0.4f);
		}
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (health < 0) {
			health = 0;
		}
		Debug.Log("received hit : " + health);


		if (health <= 0) {
			Instantiate(Resources.Load<GameObject>("gameover"), new Vector3(Camera.main.transform.position.x,
			                                                                Camera.main.transform.position.y,
			                                                                0),
			            Quaternion.identity);
			Destroy(gameObject);
		}
	}

	public void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Fireball") {
			health--;
			Debug.Log("received hit : " + health);
			healthIndicator.localScale = new Vector3(
				(health/maxHealth) * healthIndicatorX,
				healthIndicator.localScale.y,
				healthIndicator.localScale.z
				);
			Destroy(col.gameObject);
			if (health == 0) {
				Destroy(gameObject);
			}
		}
	}


}
