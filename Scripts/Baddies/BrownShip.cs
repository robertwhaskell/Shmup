using UnityEngine;
using System.Collections;

public class BrownShip : Enemy {

//	void Awake(){
//		Initialize(3, 5, 0.5f);
//	}
	
	// Use this for initialization
	void Start () {
		laserCannon = gameObject.GetComponentInChildren<EnemyLaser> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!dying){
			shotTimer += Time.deltaTime;
			LaserShoot ();
			moveFunction();
			TriggerAnimations ();
		} else if(GameManager.instance.game.possessionPoints < 1){
			shipDestroyed();
		}
		PositionInBoundaries ();
	}
}
