using UnityEngine;
using System.Collections;

public class BrownFollower : Follower {
	
	
	void Awake () {
	}
	
	// Use this for initialization
	void Start () {
		Initialize ();
		laserCannon = gameObject.GetComponentInChildren<PlayerLaser> ();
		shotDelay = 6;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.paused && player != null) {
			shotTimer += Time.deltaTime;
			LaserShoot ();
			if (alt) {
				ShieldMove ();
			} else {
				FollowerMove ();
			}
			TriggerAnimations ();
			PositionInBoundaries ();
		}
	}

	public override void InitializeAlt () {
		laserCannon.ShutDownCannon ();
		laserCannon.aimMax = 0.1f;
		laserCannon.fireMax = 2.8f;
		shotTimer = shotDelay - 0.1f;

		spaceX = GameManager.instance.laserFocusXStart;
		spaceY = -Mathf.Sqrt(Mathf.Pow (0.75f, 2f) - Mathf.Pow(GameManager.instance.laserFocusXStart, 2));
		GameManager.instance.laserFocusXStart += GameManager.instance.laserFocusXSpacing;

	}
	
	public override void UninitializeAlt () {
		laserCannon.ShutDownCannon ();
		laserCannon.aimMax = 2f;
		laserCannon.fireMax = 2f;
		shotTimer = Random.Range (0, shotDelay);
	}
}
