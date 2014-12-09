using UnityEngine;
using System.Collections;

public class ItemThrowable : MonoBehaviour {

	void OnDestroy() {
		if (Random.Range(0, 100) < 50) {
			Instantiate(Resources.Load<GameObject>("2"), transform.position, Quaternion.identity);
		}
	}
}
