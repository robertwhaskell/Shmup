using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SaveLoad.instance.Save ();
		Text pointsText = GameObject.Find ("PointsText").GetComponent<Text> ();
		pointsText.text = "Points = " + GameManager.instance.game.points;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0) || Input.GetKey ("space")) {
			GameManager.instance.game.clearGame ();
			Application.LoadLevel("StartScreen");
		}
	}
}
