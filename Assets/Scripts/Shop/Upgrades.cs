using UnityEngine;
using System.Collections;

/**
 * This component is used to store the 
 * defence and attack levels and the coins.
 **/
public class Upgrades : MonoBehaviour {

	//indicator for the defence level.
	public static int health;
	//indicator for the attack level.
	public static int damage;
	//indicator for the coins the player has.
	public static int coins;

	/**
	 * Initialize variables.
	 **/
	void Start () {
		if (!PlayerPrefs.HasKey ("health")) {
			PlayerPrefs.SetInt("health", 1);
		}
		if (!PlayerPrefs.HasKey("damage")) {
			PlayerPrefs.SetInt("damage", 1);
		}
		if (!PlayerPrefs.HasKey("coins")) {
			PlayerPrefs.SetInt("coins", 0);
		}
		health = PlayerPrefs.GetInt ("health");
		damage = PlayerPrefs.GetInt ("damage");
		coins = PlayerPrefs.GetInt ("coins");
	}

	/**
	 * This method adds increases the coins by one.
	 **/
	public static void AddCoin() {
		coins++;
		PlayerPrefs.SetInt ("coins", coins);
	}

	/**
	 * This method upgrades the defence level
	 * after checking that the available number of coins exists.
	 **/
	public static bool UpgradeHealth() {
		if (health*10 > coins) {
			return false;
		}
		coins -= health * 10;
		health++;
		PlayerPrefs.SetInt ("health", health);
		PlayerPrefs.SetInt ("coins", coins);
		return true;
	}

	/**
	 * This method upgrades the attack level
	 * after checking that the available number of coins exists.
	 **/
	public static bool UpgradeDamage() {
		if (damage * 10 > coins) {
			return false;
		}
		coins -= damage * 10;
		damage++;
		PlayerPrefs.SetInt ("damage", damage);
		PlayerPrefs.SetInt ("coins", coins);
		return true;
	}
}
