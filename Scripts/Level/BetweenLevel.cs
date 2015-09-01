using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BetweenLevel : MonoBehaviour {

	private SaveableGame game;
	// Use this for initialization
	void Start () {
		game = GameManager.instance.game;
		GameManager.instance.levelOver = false;
		
		Text levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Level = " + game.level;

		UpdatePointDisplay ();
		UpdateShotDelayDisplay ();
	}

	private void UpdatePointDisplay(){
		Text pointsText = GameObject.Find ("PointsText").GetComponent<Text> ();
		pointsText.text = "Points = " + game.points;
	}

	private void UpdateShotDelayDisplay(){
		Text shotDelayText = GameObject.Find ("ShotDelayText").GetComponent<Text> ();
		shotDelayText.text = "Shot Delay = " + game.playerShotDelay;
	}

	public void UpgradeShotDelay () {
		if (game.playerShotDelay > 0.01f && game.points > 0) {
			game.points --;
			game.playerShotDelay -= 0.001f;
			UpdatePointDisplay ();
			UpdateShotDelayDisplay ();
		}
	}

	public void GoToNextLevel() {
		SaveLoad.instance.Save ();
		Application.LoadLevel(GameManager.instance.game.level);
	}

}
