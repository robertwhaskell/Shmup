using UnityEngine;
using System.Collections;

public class RedFollower : Follower {
	
	
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
			FollowerShoot ();
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
		shotDelay /= 8;

		spaceX = GameManager.instance.focusXStart;
		spaceY = Mathf.Sqrt(Mathf.Pow (0.75f, 2f) - Mathf.Pow(GameManager.instance.focusXStart, 2));
		GameManager.instance.focusXStart += GameManager.instance.focusXSpacing;
	}

	public override void UninitializeAlt () {
		shotDelay *= 8;
	}
}

