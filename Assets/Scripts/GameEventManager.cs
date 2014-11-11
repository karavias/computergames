using UnityEngine;
using System.Collections;

public static class GameEventManager {
	public delegate void GameEvent(Vector3 e, float direction, float damage);
	public static event GameEvent HitEvent;
	
	public static void ClearAll() {
		HitEvent = null;

	}
	
	public static void TriggerHitEvent(Vector3 pos, float direction,
	                                   float damage){
		if(HitEvent != null){
			HitEvent(pos, direction, damage);
		}
	}
	
}