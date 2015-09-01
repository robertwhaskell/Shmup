using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class L1Boss : Enemy {
	
	public GameObject explosion;
	public Cannon[] guns;


	void Awake () {
		Initialize (15, 0.5f, 1);
		animator.SetFloat ("hitPoints", hp);
		guns = gameObject.GetComponentsInChildren<Cannon> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		shotTimer += Time.deltaTime;
		if (shotTimer >= shotDelay) {
			shotTimer = 0;
			BossShoot();
		}
		if (inPosition) {
			LeftToRight ();
		} else {
			GetInPosition ();
		}
		PositionInBoundaries ();
	}

	public void GetInPosition () {
		shotTimer = 0;

		float diffX = destinationX - transform.position.x;
		float diffY = destinationY - transform.position.y;

		diffX = diffX / Mathf.Abs (diffX);
		diffY = diffY / Mathf.Abs (diffY);

		float veloX = 0;
		float veloY = 0;
		if (transform.position.x > destinationX + 0.5f || transform.position.x < destinationX - 0.5f) {
			veloX = diffX * xSpeed;
		}
		if (transform.position.y > destinationY + 0.5f || transform.position.y < destinationY - 0.5f) {
			veloY = diffY * ySpeed;
		}
		rb2D.velocity = new Vector2 (veloX, veloY);
		
		if (veloX == 0 && veloY == 0) {
			inPosition = true;
		}
	}

	public void BossShoot () {
		foreach (Cannon gun in guns){
			gun.shoot ();
		}
	}

	public override void TakeDamage (float loss){
		hp --;
		if (hp <= 0) {
			GameManager.instance.levelOver = true;
			Destroy (gameObject);
		} else if (hp == 9 || hp == 4 || hp == 1){
			StartCoroutine(Explosions ());
		}
		animator.SetFloat ("hitPoints", hp);
	}

	IEnumerator Explosions () {
		for (int i = 0; i < 5; i ++){
			Instantiate(explosion,
			            new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1),
			                                   Random.Range(transform.position.y - 1, transform.position.y + 1.5f)),
			            Quaternion.identity);
			yield return new WaitForSeconds (0.1f);
		}
	}

}
