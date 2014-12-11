using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public static float remainingTime;
	public float initialTime = 10f;
	// Use this for initialization
	void Start () {
		remainingTime = initialTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (remainingTime > 0) {
			remainingTime -= Time.deltaTime;
		}
	}
}
