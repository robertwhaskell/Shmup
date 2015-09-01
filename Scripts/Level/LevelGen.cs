using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGen : MonoBehaviour {

	public GameObject player;
	public GameObject[] enemies;
	public static LevelGen instance;

	public List<GameObject> playerClip;
	public List<GameObject> enemyClip;
	public List<GameObject> missileClip;

	public List<GameObject> blueShips;
	public List<GameObject> redShips;
	public List<GameObject> brownShips;
	public List<GameObject> globeShips;

	void Awake () {
		instance = this;
		playerClip = new List<GameObject> ();
		enemyClip = new List<GameObject> ();
		missileClip = new List<GameObject> ();

		blueShips = new List<GameObject> ();
		redShips = new List<GameObject> ();
		brownShips = new List<GameObject> ();
		globeShips = new List<GameObject> ();
	}

	public GameObject placeBaddy(int index, int hp, float xsp, float ysp, Vector2 pos, string movePattern){
		
		switch (index) {
		case 0:
			if (blueShips.Count > 0) {
				return pullFromGraveyard(blueShips, hp, xsp, ysp, pos, movePattern);
			}
			break;
		case 1:
			if (redShips.Count > 0) {
				return pullFromGraveyard(redShips, hp, xsp, ysp, pos, movePattern);
			}
			break;
		case 2:
			if (brownShips.Count > 0) {
				return pullFromGraveyard(brownShips, hp, xsp, ysp, pos, movePattern);
			} 
			break;
		case 3:
			if (globeShips.Count > 0) {
				return pullFromGraveyard(globeShips, hp, xsp, ysp, pos, movePattern);
			} 
			break;
		}
		GameObject newShip = (GameObject)Instantiate (enemies [index], pos, Quaternion.identity);
		newShip.GetComponent<Enemy> ().Initialize (hp, xsp, ysp);
		newShip.GetComponent<Enemy> ().setMove (movePattern);
		return newShip;
	}

	public void addToGraveYard(GameObject inactive, int type){
		switch (type) {
		case 0:
			blueShips.Add (inactive);
			break;

		case 1:
			redShips.Add (inactive);
			break;

		case 2:
			brownShips.Add (inactive);
			break;

		case 3:
			globeShips.Add (inactive);
			break;
		}
	}

	public void addToClip (GameObject inactive) {
		switch (inactive.tag) {
		case "PlayerShot":
			playerClip.Add (inactive);
			break;
		case "EnemyShot":
			enemyClip.Add (inactive);
			break;
		}
	}

	public void addToMissileClip (GameObject missile){
		missileClip.Add (missile);
	}

	private GameObject pullFromGraveyard (List<GameObject> shipList, int hp, float xsp, float ysp, Vector2 pos, string movePattern){
		GameObject ship = shipList[0];
		shipList.RemoveAt (0);
		ship.transform.position = pos;
		ship.GetComponent<Enemy> ().Initialize (hp, xsp, ysp);
		ship.GetComponent<Enemy> ().setMove (movePattern);
		ship.SetActive (true);
		return ship;
	} 
}




