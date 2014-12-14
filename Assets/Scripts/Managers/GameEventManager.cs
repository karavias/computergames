using UnityEngine;
using System.Collections;

/**
 * This class creates game events
 * and is used to keep the structure of the game simple.
 **/
public static class GameEventManager {
	//delegate GameEvent with 3 parameters.
	public delegate void GameEvent(Vector3 e, float direction, float damage);
	//delegate GameState event with no parameters
	public delegate void GameState();

	//The event that is triggered when the player attacks.
	public static event GameEvent HitEvent;
	//The events that are triggered when the player
	//loses, wins or enters the ritual place.
	public static event GameState GameOverEvent, GameWinEvent,
									EnteredRitualRoom;

	/**
	 * Reset all event handlers.
	 * This is called on the beginning of the level
	 * and before new handlers been initialized.
	 **/
	public static void ClearAll() {
		HitEvent = null;
		GameOverEvent = null;
		GameWinEvent = null;
		EnteredRitualRoom = null;
	}

	/**
	 * Trigger a hit event with a position, direction and damage.
	 **/
	public static void TriggerHitEvent(Vector3 pos, float direction,
	                                   float damage){
		if(HitEvent != null){
			HitEvent(pos, direction, damage);
		}
	}

	/**
	 * Trigger a game over event.
	 **/
	public static void TriggerGameOverEvent() {
		if (GameOverEvent != null) {
			GameOverEvent();
		}
	}

	/**
	 * Trigger a game over with win event.
	 **/
	public static void TriggerGameWinEvent() {
		if (GameWinEvent != null) {
			GameWinEvent();
		}
	}

	/**
	 * Trigger the event that the player enters the ritual room.
	 **/
	public static void TriggerEnteredRitualRoom() {
		if (EnteredRitualRoom != null) {
			EnteredRitualRoom();
		}
	}

}