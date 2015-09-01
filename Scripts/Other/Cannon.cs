using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public GameObject shot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void shoot () {
		if (LevelGen.instance.enemyClip.Count > 0) {
			GameObject shot = LevelGen.instance.enemyClip [0];
			LevelGen.instance.enemyClip.RemoveAt (0);
			shot.transform.position = transform.position;
			shot.SetActive (true);
		} else {
			Instantiate (shot, transform.position, Quaternion.identity);
		}
	}
	
}
