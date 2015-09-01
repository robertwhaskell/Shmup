using UnityEngine;
using System.Collections;

public class DangerDetector : MonoBehaviour {

	public GameObject follower;
	// Use this for initialization
	void Start () {
		if (follower == null) {
			follower = GameObject.FindGameObjectWithTag("Follower");
		}
	}

	public void SetFollower (GameObject fol) {
		follower = fol;
	}

	// Update is called once per frame
	void Update () {
		if (follower == null) {
			Destroy (gameObject);
		} else {
			transform.position = follower.transform.position;
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy" || other.tag == "EnemyShot") {
			if (other.transform.position.x < transform.position.x){
				follower.GetComponent<Follower>().rb2D.AddForce(new Vector2(100, 0));
			} else {
				follower.GetComponent<Follower>().rb2D.AddForce(new Vector2(-100, 0));
			}
		}
	}
}
