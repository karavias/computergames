using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	static TextMesh scoreMesh;
	static int score;
	// Use this for initialization
	void Start () {
		score = 0;
		scoreMesh = GetComponent<TextMesh> ();
		AddScore (0);
	}

	public static void AddScore(int sc) {
		score += sc;
		scoreMesh.text = "Score: " + score;
	}
}
