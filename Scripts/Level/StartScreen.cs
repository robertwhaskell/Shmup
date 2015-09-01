using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void LoadGame () {
		SaveableGame game = SaveLoad.instance.Load ();
		GameManager.instance.LoadGame (game);
		Application.LoadLevel (game.level);

	}

	public void StartGame () {
		Application.LoadLevel("Level1");
	}
	// Update is called once per frame
	void Update () {

	}
}
