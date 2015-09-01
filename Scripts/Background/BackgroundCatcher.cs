using UnityEngine;
using System.Collections;

public class BackgroundCatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter2D (Collider2D other) {
		
		if (other.tag.Equals ("Background")) {
			other.transform.position = new Vector2(Random.Range(-9, 9), 15);
		}
		
	}
}
