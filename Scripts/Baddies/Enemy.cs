using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class Enemy : MovingObject {

	//move specific stuff
	public float sinTimer;
	public bool moving;
	public GameObject missile;

	//pretty much universal variables

	public EnemyLaser laserCannon;
	public float hp = 3;
	public int worth = 1;
	public int typeIndex;
	public bool dying;
	public float maxVelocity = 4;
	public string movestring;

	//Completely universal variables
	public GameObject shot;
	public Animator animator;
	public BoxCollider2D boxCol;

	public void Initialize(int health, float xsp, float ysp){
		transform.tag = "Enemy";
		rb2D = GetComponent<Rigidbody2D> ();
		rb2D.velocity = new Vector2 (0, 0);
		rb2D.drag = 0;
		animator = GetComponent<Animator> ();
		boxCol = GetComponent<BoxCollider2D> ();
		boxCol.size = new Vector2(0.1f, 0.1f);
		dying = false;
		shotTimer = 0;
		timer = 0;
		sinTimer = 0;
		moving = false;
		inPosition = false;


		if (Random.Range (0, 2) == 0) {
			goingRight = true;
		} else {
			goingRight = false;
		}
		initialX = transform.position.x;
		initialY = transform.position.y;
		hp = health;
		xSpeed = xsp;
		ySpeed = ysp;
	}

	public void setMove(String movePattern){
		movestring = movePattern;
		switch (movePattern)
		{
		case "LeftToRight":
			moveFunction = LeftToRight;
			break;

		case "UpAndDown":
			moveFunction = UpAndDown;
			break;

		case "SinWaveVertical":
			moveFunction = SinWaveVertical;
			break;
	
		case "SwoopMove":
			moveFunction = SwoopMove;
			break;
	
		case "TowardsPlayer":
			turnDelay = 0.8f;
			moveFunction = TowardsPlayer;
			break;
			
		case "MoveShootMove":
			moveFunction = MoveShootMove;
			break;
		
		case "StraightLine":
			moveFunction = StraightLine;
			break;
		}
	}

	public void Move(){
		moveFunction ();
	}

	public void Shoot(){
		
		if (shotTimer >= shotDelay) {
			if (LevelGen.instance.enemyClip.Count > 0) {
				GameObject shot = LevelGen.instance.enemyClip[0];
				LevelGen.instance.enemyClip.RemoveAt(0);
				shot.transform.position = transform.position;
				shot.SetActive (true);
			} else {
				Instantiate (shot, transform.position, Quaternion.identity);
			}
			shotTimer = 0;
		}
	}

	public void TriggerAnimations () {
		if (rb2D.velocity.x > 2) {
			animator.SetBool ("turningRight", true);
		} else if (rb2D.velocity.x <= 1) {
			animator.SetBool("turningRight", false);
		}
		if (rb2D.velocity.x < -2) {
			animator.SetBool ("turningLeft", true);
		} else if (rb2D.velocity.x >= -1) {
			animator.SetBool("turningLeft", false);
		}
	}

	public void PositionInBoundaries(){
		rb2D.velocity = new Vector3 (Mathf.Clamp(rb2D.velocity.x, -maxVelocity, maxVelocity),
		                             Mathf.Clamp(rb2D.velocity.y, -maxVelocity, maxVelocity),
		                             0);
	}

	public void OnMouseDown(){
		if (dying) {
			GameManager.instance.makeFollower(typeIndex);
			shipDestroyed();
		}
	}
	
	public virtual void TakeDamage (float loss) {
		hp -= loss;
		if (hp <= 0) {
			if (GameManager.instance.game.possessionPoints > 0){
				dying = true;
				rb2D.drag = 1;
				GetComponent<ParticleSystem>().Play();
				boxCol.size = new Vector2(0.3f, 0.3f);
				StartCoroutine(TimedDestroyShip());
			} else {
				shipDestroyed();
			}

		}
	}

	IEnumerator TimedDestroyShip(){
		yield return new WaitForSeconds (4);
		shipDestroyed ();
	}

	public void shipDestroyed () {
		GameManager.instance.enemyDestroyed(worth, typeIndex);
		LevelGen.instance.addToGraveYard (gameObject, typeIndex);
		transform.position = new Vector2 (20, 20);
		gameObject.SetActive (false);
		transform.tag = "Enemy";

	}



	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "PlayerShot" && !dying) {
			TakeDamage(1);
			other.GetComponent<Shot>().shotSpent();
		} else if (other.tag == "PlayerMissile" && !dying){
			other.GetComponent<Missile>().missileSpent();
			TakeDamage(3);
		}

	}

	public void MissileShoot(){
		if (shotTimer >= shotDelay) {
			if(LevelGen.instance.missileClip.Count > 0){
				GameObject mis = LevelGen.instance.missileClip[0];
				mis.GetComponent<Missile>().SetTarget(false);
				LevelGen.instance.missileClip.RemoveAt(0);
				mis.transform.position = transform.position;
				mis.SetActive(true);
			} else {
				GameObject mis = (GameObject)Instantiate (missile, transform.position, Quaternion.identity);
				mis.GetComponent<Missile> ().SetTarget (false);
			}
			shotTimer = 0;
		}
		
	}

	public void LaserShoot () {
		if (shotTimer >= shotDelay) {
			laserCannon.Fire();
			shotTimer = 0;
		}
	}
}
