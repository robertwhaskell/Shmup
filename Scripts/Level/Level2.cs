using UnityEngine;
using System.Collections;

public class Level2 : LevelGen {
	
	public GameObject enemy;
	
	// Use this for initialization
	void Start () {
		Instantiate (player, new Vector2(0, 0), Quaternion.identity);
		StartCoroutine (Waves());
	}
	
	IEnumerator Waves () {
		yield return new WaitForSeconds (1);
		GameObject boss = (GameObject)Instantiate (enemies [4], new Vector2 (0, 8), Quaternion.identity);
		boss.GetComponent<Enemy> ().destinationX = 0;
		boss.GetComponent<Enemy> ().destinationY = 4;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}