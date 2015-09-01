using UnityEngine;
using System.Collections;

public class Missile : MovingObject {
	

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	public void SetTarget (bool good) {
		if (good) {
			moveFunction = goodMissileMove;
			baddy = GameObject.FindGameObjectWithTag ("Enemy");
			gameObject.tag = "PlayerMissile";
		} else {
			moveFunction = badMissileMove;
			player = GameObject.FindGameObjectWithTag ("Player");
			gameObject.tag = "EnemyMissile";
		}
	}

	public void missileSpent () {
		LevelGen.instance.addToMissileClip (gameObject);
		rb2D.velocity = new Vector2 (0, 0);
		transform.position = new Vector2 (20, 20);
		gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] baddies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (baddies.Length > 0) {
			GameObject closest = baddies[0];
			float shortestDistance = Mathf.Abs(Vector3.Distance(transform.position, closest.transform.position));
			foreach (GameObject bad in baddies) {
				float dist = Mathf.Abs(Vector3.Distance(transform.position, bad.transform.position));
				if (dist < shortestDistance){
					shortestDistance = dist;
					closest = bad;
				}
			}
			baddy = closest;
		}

		timer += Time.deltaTime;
		if (timer >= lifetime) {
			timer = 0;
			missileSpent ();
		} else {
			moveFunction ();
		}
	}
}
