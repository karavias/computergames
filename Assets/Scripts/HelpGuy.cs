using UnityEngine;
using System.Collections;

public class HelpGuy : MonoBehaviour {
	GUIText helpText;
	GUITexture guiTexture;
	// Use this for initialization
	void Start () {
		helpText = GameObject.Find ("helpText").GetComponent<GUIText> ();
		guiTexture = GetComponent<GUITexture> ();
	}
	
	// Update is called once per frame
	void Update () {
		guiTexture.enabled = helpText.text.Trim () != "";
	}
}
