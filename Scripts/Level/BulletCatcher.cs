using UnityEngine;
using System.Collections;

public class BulletCatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter2D (Collider2D other) {

		if (other.tag.Equals ("PlayerShot") || other.tag.Equals ("EnemyShot")) {
			other.GetComponent<Shot> ().shotSpent ();
		} else if (other.tag.Equals ("PlayerMissile") || other.tag.Equals ("EnemyMissile")) {
			other.GetComponent<Missile>().missileSpent();
		}
	}

}
