using UnityEngine;
using System.Collections;

public class RedShip : Enemy {
	

	// Use this for initialization
	void Start () {
//		laserCannon = gameObject.GetComponentInChildren<EnemyLaser> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!dying){
			shotTimer += Time.deltaTime;
			Shoot ();
			moveFunction();
			TriggerAnimations ();
		} else if(GameManager.instance.game.possessionPoints < 1){
			shipDestroyed();
		}
		PositionInBoundaries ();
	}

}
