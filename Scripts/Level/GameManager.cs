using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.UI;  
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[System.Serializable]
public class SaveableGame {
	public int points = 0;
	public int level = 1;
	public int possessionPoints = 3;
	public float playerShotDelay = 0.1f;
//	public List<FollowerSave> savedFleet = new List<FollowerSave> ();

	public void clearGame(){
		points = 0;
		level = 1;
//		followerCount = 0;
//		savedFleet = new List<FollowerSave> ();
	}
}

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject border;

	public GameObject dangerDetector;
	public GameObject[] followers;

	public float shieldXSpacing;
	public float shieldXStart;

	public float focusXSpacing;
	public float focusXStart;

	public float laserFocusXSpacing;
	public float laserFocusXStart;

	public float altUsageTimer;
	public float altMaxDuration;
	public float altMinCoolDownTime;
	public float altChargeTime;
	public bool usingAlt;
	public bool altReady;
	public bool isALevel;

	public bool flankLeftRight;
	public bool cornerLeftRight;
	public bool levelOver;
	public bool dataSaved;
	public bool paused;

	public GameObject altCanvas;
	public Text altCounterUI;

	public Follower player;

	public SaveableGame game;

	public int[] followerTypeCount;

	public List<GameObject> followerTypes;

	public bool slowTime;

	void Awake () {

		if (instance == null) {
			instance = this;
		} else if (instance != null){
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void Start () {
		//if using test scene:
		if (EditorApplication.currentScene.Equals ("Assets/Scenes/TestScene.unity")) {
			isALevel = true;
			Instantiate(border, new Vector2(-6.77f, 0), Quaternion.identity);
			Instantiate(border, new Vector2(6.77f, 0), Quaternion.identity);
			GameObject can = (GameObject)Instantiate (altCanvas, new Vector2 (0, 0), Quaternion.identity);
			altCounterUI = can.GetComponentInChildren<Text> ();
			altCounterUI.color = Color.blue;
		}

	}

	public void OnLevelWasLoaded (int level){
		if (level >= 1 && level <= 3) {
			isALevel = true;
			Instantiate(border, new Vector2(-6.77f, 0), Quaternion.identity);
			Instantiate(border, new Vector2(6.77f, 0), Quaternion.identity);
			GameObject can = (GameObject)Instantiate (altCanvas, new Vector2 (0, 0), Quaternion.identity);
			altCounterUI = can.GetComponentInChildren<Text> ();
			altCounterUI.color = Color.blue;
		} else {
			isALevel = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (isALevel) {
			if (Input.GetMouseButtonDown(1)){
				slowTime = true;
				Time.timeScale /= 5;
			} else if (Input.GetMouseButtonUp(1)){
				slowTime = false;
				Time.timeScale *= 5;
			}
			if (levelOver) {
				BeatLevel ();
			} else {
				chargeManager();
			}
		}
	}

	private void chargeManager(){
		if (altChargeTime >= altMinCoolDownTime) {
			altReady = true;
			altCounterUI.color = Color.green;
		}
		if (usingAlt) {
			altUsageTimer -= Time.deltaTime;
			altCounterUI.text = altUsageTimer.ToString ();
		} else if (!altReady) {
			altChargeTime += Time.deltaTime;
			altCounterUI.text = altChargeTime.ToString ();
		}
		if (altReady && Input.GetKeyDown ("space")) {
			altCounterUI.color = Color.red;
			altUsageTimer = altChargeTime;
			usingAlt = true;
			altChargeTime = 0;
			setupAlts();
		}
		if ((usingAlt && altUsageTimer <= 0) || (Input.GetKeyUp ("space") && usingAlt)) {
			altCounterUI.color = Color.blue;
			altReady = false;
			usingAlt = false;
			altChargeTime = altUsageTimer;
			foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Follower")) {
				ship.GetComponent<Follower> ().UninitializeAlt ();
				ship.GetComponent<Follower> ().alt = false;
				ship.GetComponent<Follower> ().repulsion = 1;
			}
		}
	}
	
	private void setupAlts(){
		followers = GameObject.FindGameObjectsWithTag ("Follower");
		int blueCount, redCount, brownCount, globeCount;
		redCount = blueCount = brownCount = globeCount = 0;

		foreach (GameObject ship in followers) {
			switch (ship.GetComponent<Follower> ().saveData.typeIndex)
			{
			case 0:
				blueCount ++;
				break;
				
			case 1:
				redCount ++;
				break;
				
			case 2:
				brownCount++;
				break;
				
			case 3:
				globeCount++;
				break;
			}
		}

		ShieldFormation (globeCount);
		FocusFormation (redCount);
		LaserFocusFormation (brownCount);

		foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Follower")) {
			ship.GetComponent<Follower> ().InitializeAlt ();
			ship.GetComponent<Follower> ().alt = true;
			ship.GetComponent<Follower> ().repulsion = 0;
		}
	}

	private void FleetFormation () {
		GameObject[] followers = GameObject.FindGameObjectsWithTag ("Follower");
		switch (followers.Length)
		{
		case 1:
			followers[0].GetComponent<Follower>().setFleetXY(0, -0.4f);
			break;
			
		case 2:
			followers[0].GetComponent<Follower>().setFleetXY(0.2f, -0.4f);
			followers[1].GetComponent<Follower>().setFleetXY(-0.2f, -0.4f);
			break;
			
		case 3:
			followers[0].GetComponent<Follower>().setFleetXY(-0.2f, -0.4f);
			followers[1].GetComponent<Follower>().setFleetXY(0.2f, -0.4f);
			followers[2].GetComponent<Follower>().setFleetXY(0, -0.7f);
			break;
			
		case 4:
			followers[0].GetComponent<Follower>().setFleetXY(0f, -0.4f);
			followers[1].GetComponent<Follower>().setFleetXY(0.25f, -0.7f);
			followers[2].GetComponent<Follower>().setFleetXY(-0.25f, -0.7f);
			followers[3].GetComponent<Follower>().setFleetXY(0, -1f);
			break;

		case 5:
			followers[0].GetComponent<Follower>().setFleetXY(0f, -0.4f);
			followers[1].GetComponent<Follower>().setFleetXY(0.25f, -0.7f);
			followers[2].GetComponent<Follower>().setFleetXY(-0.25f, -0.7f);
			followers[3].GetComponent<Follower>().setFleetXY(-0.2f, -1.1f);
			followers[4].GetComponent<Follower>().setFleetXY(0.2f, -1.1f);
		break;
		}

	}

	private void FocusFormation(int count){
		if (count > 2) {
			focusXSpacing = 0.5f / (count - 1); 
			focusXStart = -0.25f;
		} else if (count == 1) {
			focusXSpacing = 0f;
			focusXStart = 0f;
		} else {
			focusXSpacing = 0.24f;
			focusXStart = -0.12f;
		}
	}

	private void LaserFocusFormation(int count){
		if (count > 2) {
			laserFocusXSpacing = 0.5f / (count - 1); 
			laserFocusXStart = -0.25f;
		} else if (count == 1) {
			laserFocusXSpacing = 0f;
			laserFocusXStart = 0f;
		} else {
			laserFocusXSpacing = 0.24f;
			laserFocusXStart = -0.12f;
		}
	}

	private void ShieldFormation(int count){
		if (count > 2) {
			shieldXSpacing = 1.5f / (count - 1); 
			shieldXStart = -0.75f;
		} else if (count == 1) {
			shieldXSpacing = 0f;
			shieldXStart = 0f;
		} else {
			shieldXSpacing = 0.5f;
			shieldXStart = -0.25f;
		}
	}

	public void LoadGame(SaveableGame loadedGame) {
		game = loadedGame;
	}

	private void BeatLevel () {
		levelOver = false;
		game.level ++;
		if (game.level == 3) {
			BeatGame();
		} else {
			Application.LoadLevel ("BetweenLevel");
		}
	}

	private void BeatGame () {
		Application.LoadLevel ("BeatGame");
	}

	public void PlayerGotHit () {
		game.points --;
	}

	public void PlayerDestroyed () {
		BeatGame ();
	}

	public void followerDestroyed (int loss) {
		game.points -= loss;
		FleetFormation ();

	}

	public void makeFollower (int followerType) {
		if (game.possessionPoints > 0) {
			game.possessionPoints --;
			InstantiateFollower (followerType);
			FleetFormation ();
		}
	}

	public void enemyDestroyed (int points, int followerType) {
		game.points += points;
	}

	public GameObject InstantiateFollower (int followerType){
		GameObject fol = (GameObject)Instantiate (followerTypes[followerType], new Vector3(Random.Range(-5, 6), Random.Range(-1, -4), 0), Quaternion.identity);
		return fol;
	}

//	public void SaveFleet () {
//		foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Follower")) {
//			game.savedFleet.Add(ship.GetComponent<Follower>().saveData);
//		}
//	}

//	private void addDataToFleet(GameObject ship){
//		game.savedFleet.Add(ship.GetComponent<Follower>().saveData);
//	}

//	public void loadFleet(){
//		foreach (FollowerSave data in game.savedFleet) {
//			GameObject ship = InstantiateFollower(data.typeIndex);
//			ship.GetComponent<Follower>().LoadStats(data);
//		}
//	}

	public void addPossessionPoint (){
		game.possessionPoints ++;
	}

	public void removePossessionPoint (){
		game.possessionPoints --;
	}
	
}

