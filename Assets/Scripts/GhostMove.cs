using UnityEngine;
using System.Collections;

public class GhostMove : MonoBehaviour {
	GameObject[] turnPoints;
	GameObject destination;
	GameObject previousDestination;
	public float speed = 1f;
	public float thereyet = 0.2f;
	// Use this for initialization
	void Start () {
		turnPoints = GameObject.FindGameObjectsWithTag ("turnPoint");
		destination = turnPoints [Random.Range (0, turnPoints.Length)];
		previousDestination = destination;
		transform.position = destination.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, 
		                      destination.transform.position) < thereyet) {
			previousDestination = destination;
			GameObject[] nextDest = destination.GetComponent<PathCreator>().connections;

			while (destination == previousDestination) {
				destination = nextDest[Random.Range(0, nextDest.Length)];
			}
		}
		transform.position += 
			(destination.transform.position - transform.position).normalized 
						* speed * Time.deltaTime;
	}
}
