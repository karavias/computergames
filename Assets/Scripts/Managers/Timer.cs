using UnityEngine;
using System.Collections;

/**
 * This component impelments the timer that is used by the game.
 * The player has to reach ritual room before timer goes to 0.
 **/
public class Timer : MonoBehaviour {

	//The remaining time until the spawning of the boss.
	public static float remainingTime;

	//The initial time to start the count down.
	public float initialTime = 10f;

	//When the player enteres the ritual room, we stop the count down.
	bool stopTime;

	/**
	 * Initialize variables and callbacks.
	 **/
	void Start () {
		remainingTime = initialTime;
		GameEventManager.EnteredRitualRoom += HandleEnterRoom;
	}
	
	/**
	 * On every frame reduce the remaining time with the passed time.
	 **/
	void Update () {
		if (remainingTime > 0 && !stopTime) {
			remainingTime -= Time.deltaTime;
			if (remainingTime < 0) {
				remainingTime = 0;
			}
		}
	}

	/**
	 * This is a callback that is called when the player enteres the ritual room.
	 * 
	 * The timer goes off and a help message appears to tell the player what is going on.
	 **/
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
