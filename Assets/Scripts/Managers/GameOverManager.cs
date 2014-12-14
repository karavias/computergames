using UnityEngine;
using System.Collections;
/**
 * This component is used to calculate if the player has won the game.
 **/
public class GameOverManager : MonoBehaviour {
	//array with all the final enemies, including the boss.
	GameObject[] finalEnemies;

	/**
	 * Initialize the final enemies.
	 **/
	void Start () {
		finalEnemies = GameObject.FindGameObjectsWithTag ("finalEnemies");
	}
	
	/**
	 * On every frame calculate how many of the final enemies
	 * are still alive, if none of them are, then show the
	 * game win screen.
	 **/
	void Update () {
		int aliveFinalEnemies = 0;
		foreach (GameObject enemy in finalEnemies) {
			if (enemy != null && !enemy.Equals(null)) {
				aliveFinalEnemies++;
			}
		}
		Debug.Log ("alivefinalenemies? " + aliveFinalEnemies);
		if (aliveFinalEnemies == 0) {
			Debug.Log("Loading gamewin");
			Instantiate(Resources.Load<GameObject>("gamewin"),
			            new Vector3(Camera.main.transform.position.x,
			            Camera.main.transform.position.y,
			            0), 
			            Quaternion.identity);
			enabled = false;
		}

	}
}
