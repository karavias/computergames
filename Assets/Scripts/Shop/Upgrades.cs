using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {

	public static int health;
	public static int damage;
	public static int coins;

	// Use this for initialization
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

	
	public static void AddCoin() {
		coins++;
		PlayerPrefs.SetInt ("coins", coins);
	}

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
