using UnityEngine;
using System.Collections;

/**
 * This component is used to update the
 * gui text on the top left corner of the screen
 * that displayes game informations for the player.
 **/
public class StatusUpdate : MonoBehaviour {

	//The gui text.
	GUIText guiTxt;

	/**
	 * Initialize the gui text.
	 **/
	void Start () {
		guiTxt = GetComponent<GUIText> ();
	}
	
	/**
	 * On every frame update the information about
	 * the coins, remaining time, defence and attack levels.
	 **/
	void Update () {
		guiTxt.text = "Coins: " + Upgrades.coins + "        " +
					"Time: " + Timer.remainingTime.ToString("0") + "\n" 
				+ "Defence Level: " + Upgrades.health + "\n"
				+ "Attack Level: " + Upgrades.damage;
	}
}
