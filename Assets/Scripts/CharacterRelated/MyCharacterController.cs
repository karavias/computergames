using UnityEngine;
using System.Collections;

/**
 * This component is used to handle the players input for the
 * character of the game.
 **/
public class MyCharacterController : MonoBehaviour {

	//instance of the animator of the character.
	Animator animator;
	//the original scale X of the character.
	float scaleX;
	//indicator about how much high can the character jump.
	public float jumpForce = 10f;
	//indicator about how fast the character can move.
	public float speed = 1f;
	//indicator for the vertical speed of the character.
	public float verticalSpeed = 1f;
	//indicator if the character moves left or right
	public float direction = 1;
	//the original Y position of the character before a jump.
	float preJumpY;
	//indicator about when the character jumped.
	float jumpTime;
	//indicator if the jumping still occurs
	bool jumping = false;
	//indicator for the current jumping time.
	float curJumpTime = 1f;
	//indicator if th player is currently attacking.
	bool attacking = false;
	//the most top Y position the player can go.
	GameObject topest;
	//the most bottom Y position the player can go.
	GameObject bottomest;
	//the most right X position the player can go.
	GameObject rightest;
	//the most left X position the player can go.
	GameObject leftest;
	//the maximum health of the player.
	public float maxHealth;
	//the current health of the player.
	public float health;
	//the health bar of the player.
	Transform healthIndicator;
	//the scale X of the health bar indicator.
	float healthIndicatorX;
	//the gameobject that contains the sprites for the character.
	//is children of an empty gameobject.
	Transform bodyRoot;
	//instance of the help text gui text.
	GUIText helpText;
	//if the player is in range to shop.
	bool canShop;
	//reference to the shop panel
	GameObject shop;
	//reference to the power gameobject that animates the level of the player
	//with particle system.
	Transform power;

	/**
	 * First thing to do, clear all game events.
	 **/
	void Awake() {
		GameEventManager.ClearAll ();
	}

	/**
	 * Initialize all varialbes.
	 **/
	void Start () {
		canShop = false;
		shop = null;
		bodyRoot = transform.FindChild ("bodyRoot");
		maxHealth = maxHealth + Upgrades.health * 2;
		health = maxHealth;
		scaleX = transform.localScale.x;
		animator = GetComponentInChildren<Animator> ();
		topest = GameObject.Find ("Topest");
		bottomest = GameObject.Find ("Bottomest");
		rightest = GameObject.Find ("RightestCharacter");
		leftest = GameObject.Find ("LeftestCharacter");
		healthIndicator = transform.FindChild("bodyRoot").FindChild("healthbar").FindChild ("indicator");
		healthIndicatorX = healthIndicator.localScale.x;
		helpText = GameObject.Find ("helpText").GetComponent<GUIText>();
		power = bodyRoot.FindChild ("power");
		UpdatePowerColor ();
	}

	/**
	 * This function upgrades the health of the player.
	 * This is called when the player buys a defence upgrade.
	 **/
	public void UpgradeHealth() {
		maxHealth = 10 + Upgrades.health * 2;
		health = maxHealth;
		UpdatePowerColor ();
	}

	/**
	 * This function updates the visibility of the particle system.
	 * It is more visible when you increase levels.
	 **/
	public void UpdatePowerColor() {
		power.particleSystem.startSize = (Upgrades.health + Upgrades.damage) * 0.05f;
	}

	/**
	 * On every frame, handle player input.
	 **/
	void Update () {
		//open the shop if the player presses C and is near the shop.
		if (Input.GetKeyDown(KeyCode.C) && canShop && shop == null) {
			shop = Instantiate(Resources.Load<GameObject>("upgradesShop"), new Vector3(Camera.main.transform.position.x,
			                                                                    Camera.main.transform.position.y,
			                                                                    0),
			            Quaternion.identity) as GameObject;
			return;
		} else if (Input.GetKeyDown(KeyCode.C) && shop != null) {
			canShop = true;
			Destroy(shop);
			shop = null;


		} else if (shop != null) {
			return;
		}

		//if player presses escape, open the pause menu.
		if (Input.GetKeyDown(KeyCode.Escape)
		    && Time.timeScale != 0) {
			Instantiate(Resources.Load<GameObject>("pause"),
			            new Vector3(Camera.main.transform.position.x,
			            			Camera.main.transform.position.y,
			            		0)
			            ,
			            Quaternion.identity);
			Time.timeScale = 0;
		}
		//play the walk animation when the player is moving.
		animator.SetBool ("walk", Input.GetAxis ("Horizontal") != 0
		                  || Input.GetAxis("Vertical") != 0);
		//check direction of the player and update the scale X of the character.
		//so he looks like he turns.
		float sign;
		if (Input.GetAxis("Horizontal") < 0) {
			direction = -1;
		} else if (Input.GetAxis("Horizontal") > 0) {
			direction = 1;
		}
		transform.localScale = new Vector3(direction*scaleX,
		                                   transform.localScale.y,
		                                   transform.localScale.z);
		//if we are jumping, calculate the animated position 
		//of the sprites using Mathf.Sin.
		if (jumping) {
			bodyRoot.localPosition = 
				new Vector3(
					0,
					preJumpY + jumpForce * Mathf.Sin(2 * Mathf.PI * (Time.time - jumpTime) / (curJumpTime * 2)),
					0
					);
			if (Time.time - jumpTime >= curJumpTime) {
				jumping = false;
				bodyRoot.localPosition = new Vector3(0, preJumpY, 0);
			}
		}
		//update the position of the character and be sure he is between the limits
		//he can move.
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

		//update all the sprite renderers of the character according to the y position.
		foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>()) {
			rend.sortingOrder = 1000 - (int)(transform.position.y * 100);
			power.renderer.sortingOrder = rend.sortingOrder;
		}


		//if we press X the and not attacking already,
		//then we play the attack animation.
		if (Input.GetKeyDown(KeyCode.X) && !attacking) {
			animator.SetTrigger("attack");
		}
		//if we press Z and we are not jumping already,
		//we jump.
		if (Input.GetKeyDown(KeyCode.Z) && !jumping) {
			//animator.SetTrigger("jump");
			preJumpY = bodyRoot.localPosition.y;
			jumpTime = Time.time;
			jumping = true;
		}
		//update the health indicator
		healthIndicator.localScale = new Vector3(
			(health/maxHealth) * healthIndicatorX,
			healthIndicator.localScale.y,
			healthIndicator.localScale.z
			);

	}

	/**
	 * Called by the attack animation to indicate that the attack animation started.
	 **/
	public void AttackStarted() {
		attacking = true;
	}

	/**
	 * Called by the attack animation to indicate that the attack animation ended.
	 **/
	public void AttackEnded() {
		attacking = false;
	}

	/**
	 * Called by the attack animation to indicate that NOW we should check for damages.
	 **/
	public void TriggerHitEvent() {
		GameEventManager.TriggerHitEvent(transform.position, direction, 1);
	}

	/**
	 * We check here for collisions with triggered colliders.
	 **/
	public void OnTriggerEnter2D(Collider2D col) {

		if (col.gameObject.tag == "RitualRoom") {
			//if the character enters the ritual room. Trigger the event.
			GameEventManager.TriggerEnteredRitualRoom();
		} else if (col.gameObject.tag == "coin") {
			//if the character received a coin, add it to the Upgrades.
			//and destroy the coin.
			Upgrades.AddCoin();
			Destroy(col.gameObject);
		}
		if (col.gameObject.tag == "Fireball") {
			//if the character collides with a fireball and not jumping, apply damage.
			if (jumping) {
				return;
			}
			applyDamage(1, Mathf.Sign(transform.position.x - col.transform.position.x));
			Destroy(col.gameObject);
		}
		if (col.gameObject.tag == "Arrow") {
			//if the character collides with an arrow and not jumping, apply damage.
			if (jumping) {
				return;
			}
			applyDamage(2, Mathf.Sign(transform.position.x - col.transform.position.x));
			Destroy (col.gameObject);
		}
		//if the character collides with a food, apply negative damage.
		if (col.gameObject.tag == "healthup") {
			applyDamage(-1, 1);
			Destroy(col.gameObject);
		}
		//if the character is near the shop. Enable shop.
		if (col.gameObject.tag == "seller") {
			canShop = true;
			helpText.text = "Press C to buy upgrades, and again C to close the shop";
		}
	}

	/**
	 * We use this function to know when we go away from the shop.
	 **/
	public void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "seller") {
			helpText.text = "";
			canShop = false;
		}
	}

	/**
	 * Applies the damage to the player.
	 **/
	public void applyDamage(int damage, float direction) {
		if (damage > 0) {
			//if the player is currently jumping, no damage is applied.
			if (jumping) {
				return;
			}
			//display a cartoon "pow" effect.
			Destroy(Instantiate(Resources.Load<GameObject>("pow"), 
			                    transform.position +
			                    new Vector3(Random.Range(-0.2f, 0.2f),
			            0.5f + Random.Range(-0.1f, 0.2f),
			            0)
			                    , Quaternion.identity), 0.4f);
			//add force to throw character a bit away from the collision.
			rigidbody2D.AddForce(new Vector2(direction*100, 0), ForceMode2D.Impulse);
		}
		//calculate new health.
		health -= damage;
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (health < 0) {
			health = 0;
		}

		//if health is 0 or less, player loses.
		if (health <= 0) {
			Instantiate(Resources.Load<GameObject>("gameover"), new Vector3(Camera.main.transform.position.x,
			                                                                Camera.main.transform.position.y,
			                                                                0),
			            Quaternion.identity);
			Destroy(gameObject);
		}
	}

	/**
	 * We use this to destroy colliders that we want to be removed
	 * when the player touches them. But not when the enemies touch them.
	 **/
	public void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "touchDestroy") {
			Destroy(col.gameObject);
		}
	}


}
