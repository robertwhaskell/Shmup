using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float speed;
	
	private Rigidbody2D rb2D;
	// Use this for initialization
	void Start () {
		rb2D = gameObject.GetComponent<Rigidbody2D> ();
		rb2D.velocity = new Vector2 (0, speed);
	}

}
