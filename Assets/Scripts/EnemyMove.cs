using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {


	private GameObject target;
	public float moveSpeed;
	private float posY;
	private float posX; 
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("MyPlayer");
		moveSpeed = 0.03F;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector2.Distance (transform.position, target.transform.position);
		if(distance < 5.0F)
		{
			enemyMoveMethod();
		}

	}


	void enemyMoveMethod() {
		if(transform.position.y > target.transform.position.y+0.1F) {
			posY = transform.position.y - (moveSpeed/2);
			//Debug.Log ("Move down");
		}
		else if(transform.position.y < target.transform.position.y-0.1F) {
			posY = transform.position.y + (moveSpeed/2);
			//Debug.Log ("Move up");
		}
		
		if(transform.position.x > target.transform.position.x + 1) {
			posX = transform.position.x - moveSpeed;
		}
		else if(transform.position.x < target.transform.position.x - 1) {
			posX = transform.position.x + moveSpeed;
		}
		transform.position = new Vector3(posX, posY, transform.position.z);
	
	}
}
