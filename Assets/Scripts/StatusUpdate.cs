using UnityEngine;
using System.Collections;

public class StatusUpdate : MonoBehaviour {

	GUIText guiTxt;
	// Use this for initialization
	void Start () {
		guiTxt = GetComponent<GUIText> ();
	}
	
	// Update is called once per frame
	void Update () {
		guiTxt.text = "Coins: " + Upgrades.coins + "        " +
					"Time: " + Timer.remainingTime.ToString("0") + "\n" 
				+ "Health Level: " + Upgrades.health + "\n"
				+ "Strenght Level: " + Upgrades.damage;
	}
}
