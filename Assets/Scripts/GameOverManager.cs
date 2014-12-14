using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
	GameObject[] finalEnemies;
	// Use this for initialization
	void Start () {
		finalEnemies = GameObject.FindGameObjectsWithTag ("finalEnemies");
	}
	
	// Update is called once per frame
	void Update () {
		int aliveFinalEnemies = 0;
		foreach (GameObject enemy in finalEnemies) {
			if (enemy != null && !enemy.Equals(null)) {
				aliveFinalEnemies++;
			}
		}
		if (aliveFinalEnemies == 0) {
			Instantiate(Resources.Load<GameObject>("gamewin"), 
			            Camera.main.transform.position, 
			            Quaternion.identity);
			enabled = false;
		}
	}
}
