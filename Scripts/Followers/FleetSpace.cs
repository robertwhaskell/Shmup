using UnityEngine;
using System.Collections;

public class FleetSpace : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			transform.position = new Vector2 (player.transform.position.x, player.transform.position.y - 0.9f);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag.Equals ("Follower")) {
			other.GetComponent<Follower>().inPosition = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag.Equals ("Follower")) {
			other.GetComponent<Follower>().inPosition = false;
		}

	}
}
