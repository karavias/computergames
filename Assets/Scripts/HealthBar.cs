using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	Destroyable destr;
	float initX;
	Transform root;
	// Use this for initialization
	void Start () {
		root = transform.root;
		destr = transform.root.GetComponent<Destroyable> ();
		initX = transform.localScale.x;

	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3 (
			destr.GetPercent () * initX, transform.localScale.y,
		                                   transform.localScale.z);

		transform.parent.localScale = new Vector3(Mathf.Sign(root.localScale.x) * Mathf.Abs(transform.parent.localScale.x),
		                                                      transform.parent.localScale.y,
		                                                      transform.parent.localScale.z);
		
	}
}
