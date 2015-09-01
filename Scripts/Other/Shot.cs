using UnityEngine;
using System.Collections;

public class Shot : MovingObject {
	
	private AudioSource shotAudio;

	void OnEnable(){
		rb2D = GetComponent<Rigidbody2D> ();
		shotAudio = GetComponent<AudioSource> ();
		shotAudio.Play ();

	}

	void Update () {
		ShotMove ();
	}

	public void shotSpent () {
		LevelGen.instance.addToClip (gameObject);
		transform.position = new Vector2 (20, 20);
		gameObject.SetActive (false);
		
	}
	
}
