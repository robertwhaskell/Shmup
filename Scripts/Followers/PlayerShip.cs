using UnityEngine;
using System.Collections;

public class PlayerShip : Follower {

	// Use this for initialization
	void Start () {
		PlayerInit ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.paused) {
			shotTimer += Time.deltaTime;
			if (Input.GetMouseButton(0)){
				FollowerShoot();
			}
			PlayerMove();
			TriggerAnimations ();
			PositionInBoundaries ();
		}
	}
	
	public override void InitializeAlt () {
		shotDelay /= 4;
	}
	
	public override void UninitializeAlt () {
		shotDelay *= 4;
	}
}
