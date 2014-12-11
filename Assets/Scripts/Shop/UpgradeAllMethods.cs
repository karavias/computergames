using UnityEngine;
using System.Collections;

public class UpgradeAllMethods : MonoBehaviour {
	public bool showHealth;
	public bool showDamage;
	public bool showHealthCost;
	public bool showDamageCost;
	public bool clickHealth;
	public bool clickDamage;
	GUIText txt;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("MyPlayer");
		txt = GameObject.Find ("helpText").GetComponent<GUIText> ();
		if (showHealth) {
			GetComponent<TextMesh> ().text = "Current level : " + Upgrades.health;
		} else if (showDamage) {
			GetComponent<TextMesh>().text = "Current level : " + Upgrades.damage;
		} else if (showHealthCost) {
			GetComponent<TextMesh>().text = "Upgrade for " + (Upgrades.health * 10) + " coins";
		} else if (showDamageCost) {
			GetComponent<TextMesh>().text = "Upgrade for " + (Upgrades.damage * 10) + " coins";
		}

	}

	void OnMouseDown() {
		if (clickHealth) {
			if (!Upgrades.UpgradeHealth()) {
				txt.text = "Not enough coins...";
			} else {
				player.SendMessage("UpgradeHealth");
				txt.text = "Shield Upgraded!!!";
			}
		} else if (clickDamage) {
			if (!Upgrades.UpgradeDamage()) {
				txt.text = "Not enough coins...";
			} else {
				player.SendMessage("UpdatePowerColor");
				txt.text = "Sword Upgraded!!!";
			}
		}
	}
}
