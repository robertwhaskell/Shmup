using UnityEngine;
using System.Collections;

public class GlobeShip : Enemy {

	public float hitsTaken;
	public float hitRegenTimer;
	public float regenTimerMax;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hitRegenTimer <= regenTimerMax) {
			hitRegenTimer += Time.deltaTime;
		} else if(hitsTaken > 0) {
			hitsTaken -= 0.5f;
			hitRegenTimer = 0;
		}
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

//	void OnTriggerEnter2D(Collider2D other){
//		if (other.tag == "Follower" || other.tag == "Player" || other.tag == "PlayerShot" || other.tag == "PlayerMissile") {
//			hitsTaken += 0.5f;
//			hitRegenTimer = 0;
//			if (other.tag == "PlayerShot") {
//				other.GetComponent<Shot> ().shotSpent ();
//			} else if (other.tag == "PlayerMissile") {
//				other.GetComponent<Missile>().missileSpent();
//			}
//			if (hitsTaken > 3){
//				TakeDamage(10000);
//			}
//
//		}
//	}
}
