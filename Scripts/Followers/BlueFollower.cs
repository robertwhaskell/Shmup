using UnityEngine;
using System.Collections;

public class BlueFollower : Follower {


	void Awake () {
	}

	// Use this for initialization
	void Start () {
		Initialize ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.paused && player != null) {
			shotTimer += Time.deltaTime;
			MissileShoot ();
			if (alt) {
				FlankMove ();
			} else {
				FollowerMove ();
			}
			TriggerAnimations ();
			PositionInBoundaries ();
		}
	}

	public override void InitializeAlt () {
		spaceY = 0.50f;
		shotDelay /= 8;
		if (GameManager.instance.flankLeftRight) {
			spaceX = 0.75f;
			GameManager.instance.flankLeftRight = false;
		} else {
			spaceX = -0.75f;
			GameManager.instance.flankLeftRight = true;
		}
	}
	
	public override void UninitializeAlt () {
		shotDelay *= 8;
	}
}
