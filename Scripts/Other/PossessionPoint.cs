using UnityEngine;
using System.Collections;

public class PossessionPoint : MonoBehaviour {

	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, player.transform.position, 0.3f);
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag.Equals ("Player")) {
			GameManager.instance.addPossessionPoint();
			Destroy(gameObject);
		}
	}
}
