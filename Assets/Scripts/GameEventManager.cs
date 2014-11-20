using UnityEngine;
using System.Collections;

public static class GameEventManager {
	public delegate void GameEvent(Vector3 e, float direction, float damage);
	public delegate void GameState();
	public static event GameEvent HitEvent;
	public static event GameState GameOverEvent, GameWinEvent,
									EnteredRitualRoom;

	public static void ClearAll() {
		HitEvent = null;
		GameOverEvent = null;
		GameWinEvent = null;
		EnteredRitualRoom = null;
	}
	
	public static void TriggerHitEvent(Vector3 pos, float direction,
	                                   float damage){
		if(HitEvent != null){
			HitEvent(pos, direction, damage);
		}
	}

	public static void TriggerGameOverEvent() {
		if (GameOverEvent != null) {
			GameOverEvent();
		}
	}

	public static void TriggerGameWinEvent() {
		if (GameWinEvent != null) {
			GameWinEvent();
		}
	}

	public static void TriggerEnteredRitualRoom() {
		if (EnteredRitualRoom != null) {
			EnteredRitualRoom();
		}
	}

}