using UnityEngine;
using System.Collections;

/**
 * This component is used by the upgrade panel
 * to display information about the upgrades
 * and provides functiality to purchase and upgrade.
 **/
public class UpgradeAllMethods : MonoBehaviour {
	//indicator if this object shows the defence level.
	public bool showHealth;
	//indicator if this object shows the attack level.
	public bool showDamage;
	//indicator if it this object shows the defence upgrade cost.
	public bool showHealthCost;
	//indicator if this object sohws the attack upgrade cost.
	public bool showDamageCost;
	//indicator if this object is the upgrade button for defence.
	public bool clickHealth;
	//indicator if this object is the upgrade button for attack.
	public bool clickDamage;
	//reference to the help text gui Texture.
	GUIText txt;
	//reference to the player's gameobject.
	GameObject player;

	/**
	 * Initialize and show information about the levels and costs.
	 **/
	void Start () {
		player = GameObject.FindWithTag ("MyPlayer");
		txt = GameObject.Find ("helpText").GetComponent<GUIText> ();


	}

	void Update() {
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

	/**
	 * If it is an upgrade button.
	 * Handle it here.
	 **/
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
