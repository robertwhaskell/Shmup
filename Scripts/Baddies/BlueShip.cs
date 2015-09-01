using UnityEngine;
using System.Collections;

public class BlueShip : Enemy {

	void Awake(){
		Initialize(3, 100, 100);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!dying){
			shotTimer += Time.deltaTime;
			MissileShoot();
			moveFunction();
			TriggerAnimations ();
		} else if(GameManager.instance.game.possessionPoints < 1){
			shipDestroyed();
		}
		PositionInBoundaries ();
	}
}
