using UnityEngine;
using System.Collections;

public class RitualEntered : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameEventManager.EnteredRitualRoom += OnEnterRitualRoom;
	}

	void OnEnterRitualRoom() {
		if (Timer.remainingTime > 0) {
			//todo: disrupt rituals
			Debug.Log("disrupt rituals");
		} else {
			//todo: spawn boss
			Debug.Log("spawn boss");
		}
	}

}
