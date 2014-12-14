using UnityEngine;
using System.Collections;

/**
 * This component is used to show/hide the character
 * on the bottom left corner of the screen that gives some hints to the player.
 **/
public class HelpGuy : MonoBehaviour {
	//The gui text that displayes the text message.
	GUIText helpText;
	//the gui texture of the helper character.
	GUITexture guiTexture;

	/**
	 * Initialize variables.
	 **/
	void Start () {
		helpText = GameObject.Find ("helpText").GetComponent<GUIText> ();
		guiTexture = GetComponent<GUITexture> ();
	}
	
	/**
	 * On every frame check if the help text contains any characters.
	 * If yes, then set character's texture to visible. 
	 * Otherwise to not visible.
	 **/
	void Update () {
		guiTexture.enabled = helpText.text.Trim () != "";
	}
}
