﻿using UnityEngine;
using System.Collections;

/*
 *0 = blue 100, 100
 *1 = red 1, -4
 *2 = brown 5, 0.5
 *3 = globe 2, 0.8 
 *4 = boss 0.5, 1
*/
public class TestLevel : LevelGen {
	
	void Start () {
		Instantiate (player, new Vector2(0, 0), Quaternion.identity);
		StartCoroutine (Waves());
	}

	IEnumerator Waves () {
		yield return new WaitForSeconds (1);
		GameObject ship = (GameObject)placeBaddy(1, 1, 2.5f, -0.5f, new Vector2(-5, 3), "StraightLine");

	}
	
	
}