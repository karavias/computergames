using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public static float remainingTime;
	public float initialTime = 10f;
	bool stopTime;
	// Use this for initialization
	void Start () {
		remainingTime = initialTime;
		GameEventManager.EnteredRitualRoom += HandleEnterRoom;
	}
	
	// Update is called once per frame
	void Update () {
		if (remainingTime > 0 && !stopTime) {
			remainingTime -= Time.deltaTime;
			if (remainingTime < 0) {
				remainingTime = 0;
			}
		}
	}

	void HandleEnterRoom() {
		stopTime = true;
		GameEventManager.EnteredRitualRoom -= HandleEnterRoom;
		if (remainingTime > 0) {
			GameObject.Find("helpText").GetComponent<GUIText>().text = "Ritual disrupted on time!!! Destroy the bad guys!";
		} else {
			GameObject.Find("helpText").GetComponent<GUIText>().text = "Oh no! You didn't make it on time... Shoggoth rises, destroy him!!!";
		}
	}


}
