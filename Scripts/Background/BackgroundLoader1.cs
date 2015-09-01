using UnityEngine;
using System.Collections;

public class BackgroundLoader1 : MonoBehaviour {

	public GameObject star;
	public GameObject bluePlanet;
	public GameObject brownPlanet;

	// Use this for initialization
	void Start () {
		Instantiate(bluePlanet, new Vector2(Random.Range(-9, 10), 15), Quaternion.identity);
		for (int i = 0; i < 25; i++) {
			Instantiate(star, new Vector2(Random.Range(-9, 10), Random.Range(-10, 2.5f)), Quaternion.identity);
			Instantiate(star, new Vector2(Random.Range(-9, 10), Random.Range(2.5f, 15)), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
