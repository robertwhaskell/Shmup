using UnityEngine;
using System.Collections;

public class GlobeFollower : Follower {
	public GameObject repulsor;
	private GameObject myRepulsor;

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
			if (alt) {
				ShieldMove ();
			} else {
				FollowerMove ();
				FollowerShoot ();
			}
			TriggerAnimations ();
			PositionInBoundaries ();
		}
	}

	public override void InitializeAlt () {
		myRepulsor = (GameObject)Instantiate (repulsor, transform.position, Quaternion.identity);
		myRepulsor.GetComponent<Repulsor> ().setParent (gameObject);
		myRepulsor.gameObject.SetActive (true);
		spaceX = GameManager.instance.shieldXStart;
		spaceY = Mathf.Sqrt(Mathf.Pow (0.75f, 2f) - Mathf.Pow(GameManager.instance.shieldXStart, 2));
		GameManager.instance.shieldXStart += GameManager.instance.shieldXSpacing;
	}
	
	public override void UninitializeAlt () {
		Destroy (myRepulsor);
	}
}
