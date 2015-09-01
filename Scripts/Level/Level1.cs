using UnityEngine;
using System.Collections;
/*
 *0 = blue 100, 100
 *1 = red 1, -4
 *2 = brown 5, 0.5
 *3 = globe 2, 0.8 
 *4 = boss 0.5, 1
*/
public class Level1 : LevelGen {

	void Start () {
		Instantiate (player, new Vector2(0, 0), Quaternion.identity);
		StartCoroutine (Waves());
	}
	
	IEnumerator Waves () {
		yield return new WaitForSeconds (3);
		for (int i = 0; i <= 20; i ++) {
			yield return new WaitForSeconds(0.3f);
					placeBaddy(2, 2, 1, -4, new Vector2(-9, 11), "SwoopMove");

		}
		for (int i = 0; i <= 20; i ++){
			yield return new WaitForSeconds(0.3f);
			GameObject ship = placeBaddy(3, 2, 1, -4, new Vector2(9, 11), "SwoopMove");
			ship.GetComponent<Enemy>().swoopingLeft = true;
			ship.GetComponent<Enemy>().xSpeed = -(ship.GetComponent<Enemy>().xSpeed);
		}
		yield return new WaitForSeconds (5);
		float pos = -8;

		for (int i = 0; i <= 5; i++){
			yield return new WaitForSeconds(0.5f);
			placeBaddy(0, 5, 1, 2, new Vector2(-4, 6), "SinWaveVertical");
			placeBaddy(0, 5, -1, 2, new Vector2(4, 6), "SinWaveVertical");
		}
		for (int i = 0; i <= 20; i ++) {
			if (pos > 8){
				pos = -8;
			}
			yield return new WaitForSeconds(0.5f);
			GameObject ship = placeBaddy(1, 3, 4, 4, new Vector2(0, 6), "MoveShootMove");
			ship.GetComponent<Enemy>().destinationX = pos;
			ship.GetComponent<Enemy>().destinationY = 4;
			pos += 1;
		}
		yield return new WaitForSeconds (5);
		pos = -5;
		for (int i = 0; i <= 30; i ++) {
			if (pos > 5){
				pos = -5;
			}
			yield return new WaitForSeconds(0.5f);
			placeBaddy(3, 5, 5, 0.5f, new Vector2(pos, 6), "TowardsPlayer");
			pos += 1;
		}
		yield return new WaitForSeconds (8);
		for (int i = 0; i < 5; i ++) {
			yield return new WaitForSeconds(0.5f);
			GameObject ship = placeBaddy(0, 5, 1, 1, new Vector2(0, 6), "SinWaveVertical");
			if (i >= 3){
				placeBaddy(0, 2, 1, 2, new Vector2(1, 6), "SinWaveVertical");
				placeBaddy(0, 2, -1, 2, new Vector2(-1, 6), "SinWaveVertical");
			}
		}
		yield return new WaitForSeconds (1);
		GameObject boss = (GameObject)Instantiate (enemies [4], new Vector2 (0, 8), Quaternion.identity);
		boss.GetComponent<Enemy> ().destinationX = 0;
		boss.GetComponent<Enemy> ().destinationY = 4;
	}


}