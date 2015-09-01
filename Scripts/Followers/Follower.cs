using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class FollowerSave {
	public float hp;
	public string tag;
	public int typeIndex;
}

public class Follower : MovingObject {

	public int worth = 1;
	public float speed = 1f;
	public float rightBoundary = 3f;
	public float leftBoundary = 3f;
	public GameObject shot;
	public float repulsion = 1;
	public int typeIndex;

	public GameObject missile;
	public bool alt;
	
	private Animator animator;
	public FollowerSave saveData;
	
	public PlayerLaser laserCannon;

	public void PlayerInit(){
		animator = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		saveData.hp = 10;
		saveData.tag = transform.tag;
		shotDelay = GameManager.instance.game.playerShotDelay;
	}

	public void Initialize () {

		animator = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		
		if (Random.Range (0, 2) == 0) {
			goingRight = true;
		} else {
			goingRight = false;
		}
		
		saveData.hp = 3;
		saveData.tag = transform.tag;
		
		rightBoundary = Random.Range (3, 5) + transform.position.x;
		leftBoundary = Random.Range (3, 5) - transform.position.x;
		speed = Random.Range (100, 200);

		switch (typeIndex)
		{
		case 0:
			moveFunction = FlankMove;
			break;
			
		case 1:
			moveFunction = FocusMove;
			break;
			
		case 2:
			moveFunction = BottomCornerMove;
			break;
			
		case 3:
			moveFunction = ShieldMove;
			break;
		}
	}

	public void setFleetXY(float x, float y){
		fleetX = x;
		fleetY = y;
	}

	public void PositionInBoundaries () {
		transform.position = new Vector3 (Mathf.Clamp(transform.position.x, -4, 4), Mathf.Clamp(transform.position.y, -4.5f, 5), 0);
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

	public void PlayerMove () {

		int horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		int vertical = (int) (Input.GetAxisRaw ("Vertical"));
		
		rb2D.AddForce (new Vector2 (horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime));

	}

//	public void PlayerShoot () {
//		if (shotTimer >= shotDelay) {
//			if(LevelGen.instance.playerClip.Count > 0){
//				GameObject shot = LevelGen.instance.playerClip[0];
//				LevelGen.instance.playerClip.RemoveAt(0);
//				shot.transform.position = transform.position;
//				shot.SetActive(true);
//			} else {
//				Instantiate(shot, transform.position, Quaternion.identity);
//			}
//			
//			shotTimer = 0;
//		}
//	}

	public void LaserShoot () {
		if (shotTimer >= shotDelay) {
			laserCannon.Fire();
			shotTimer = 0;
		}
	}

	public void FollowerShoot() {
		if (shotTimer >= shotDelay) {
			if(LevelGen.instance.playerClip.Count > 0){
				GameObject shot = LevelGen.instance.playerClip[0];
				LevelGen.instance.playerClip.RemoveAt(0);
				shot.transform.position = transform.position;
				shot.SetActive(true);
			} else {
				Instantiate(shot, transform.position, Quaternion.identity);
			}
			
			shotTimer = 0;
		}
	}

	public void MissileShoot(){
		if (shotTimer >= shotDelay) {
			if(LevelGen.instance.missileClip.Count > 0){
				GameObject mis = LevelGen.instance.missileClip[0];
				mis.GetComponent<Missile>().SetTarget(true);
				LevelGen.instance.missileClip.RemoveAt(0);
				mis.transform.position = transform.position;
				mis.SetActive(true);
			} else {
				GameObject mis = (GameObject)Instantiate (missile, transform.position, Quaternion.identity);
				mis.GetComponent<Missile> ().SetTarget (true);
			}
			shotTimer = 0;
		}

	}

	public void TakeDamage (float loss) {
		saveData.hp -= loss;
		if (saveData.hp <= 0) {
			UninitializeAlt();
			if (transform.tag == "Player") {
				GameManager.instance.PlayerDestroyed ();
			}
			transform.tag = "Untagged";
			GameManager.instance.game.possessionPoints ++;
			Destroy(gameObject);
			GameManager.instance.followerDestroyed(worth);
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			float dam = other.GetComponent<Enemy>().hp;
			other.GetComponent<Enemy>().TakeDamage(saveData.hp);
			TakeDamage(dam);
		}else if (other.tag == "EnemyShot") {
			TakeDamage (1);
			other.GetComponent<Shot>().shotSpent();
		} else if (other.tag == "EnemyMissile") {
			TakeDamage (3);
			other.GetComponent<Missile>().missileSpent();
		} else if (transform.tag != "Player " && other.tag != "PlayerShot"){
			rb2D.AddForce (new Vector2 ((transform.position.x - other.gameObject.transform.position.x) * repulsion, 
			                            (transform.position.y - other.gameObject.transform.position.y) * repulsion));
		}

	}

//	void OnMouseOver(){
//		if (GameManager.instance.paused && !transform.tag.Equals("Player") && Input.GetMouseButtonUp (1)) {
//			GameObject.FindGameObjectWithTag("Player").GetComponent<Follower>().BecomeFollower();
//			BecomePlayer();
//			foreach (GameObject fol in GameObject.FindGameObjectsWithTag("Follower")){
//				fol.GetComponent<Follower>().player = gameObject;
//			}
//		}
//	}

	public void BecomeFollower() {
		if (alt) {
			alt = false;
			UninitializeAlt();
		}
		transform.tag = "Follower";
		saveData.tag = "Follower";
		shotDelay = 1;
		rb2D.drag = 0;
		speed = 1;

	}

//	public void BecomePlayer () {
//		transform.tag = "Player";
//		saveData.tag = "Player";
//		shotDelay = .1f;
//		rb2D.drag = 7;
//		speed = 5000;
//		pSys.Play ();
//		GameManager.instance.player = GetComponent<Follower> ();
//	}

	public void LoadStats (FollowerSave data) {
		saveData = data;
		transform.tag = saveData.tag;
	}

	public virtual void InitializeAlt(){}
	public virtual void UninitializeAlt(){}
}
