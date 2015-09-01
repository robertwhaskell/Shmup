using UnityEngine;
using System.Collections;

public class SmallExplosion : MonoBehaviour {

	public Animation anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		StartCoroutine (DestroyAfterSecond());
	}

	IEnumerator DestroyAfterSecond (){
		yield return new WaitForSeconds(anim.clip.length);
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
