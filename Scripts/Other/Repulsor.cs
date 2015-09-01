using UnityEngine;
using System.Collections;

public class Repulsor : MonoBehaviour {

	private GameObject parent;
	// Use this for initialization
	void Start () {
	}

	public void setParent(GameObject globe){
		parent = globe;
	}

	// Update is called once per frame
	void Update () {
		transform.position = parent.transform.position;
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag.Equals ("Enemy") || other.tag.Equals("EnemyShot")) {
			other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.1f));
		}
	}
}
