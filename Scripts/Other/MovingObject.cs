using UnityEngine;
using System.Collections;
using System;

public class MovingObject : MonoBehaviour {

	//Follower variables
	public float fleetX;
	public float fleetY;
	public Action moveFunction;
	public GameObject player;
	public GameObject fleetSpace;
	public GameObject baddy;
	public Rigidbody2D rb2D;
	public float spaceX = 0;
	public float spaceY = 0;

	//Baddy variables
	public float sinTurnFreq = 1;
	public float moveBoundary;
	public float ySpeed;
	public float xSpeed;
	public float initialY;
	public float initialX;
	public float timer;
	public float lifetime;
	public float shotTimer;
	public float pauseDelay;
	public float turnDelay;
	public float shotDelay;
	public float destinationX;
	public float destinationY;
	public bool goingRight;
	public bool goingUp;
	public bool swoopingLeft;
	public bool inPosition;


	public void FollowerMove () {
		float range = 0.1f;
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + fleetX, player.transform.position.y + fleetY), 1);

		if (transform.position.x <= player.transform.position.x + fleetX + range 
		    && transform.position.x >= player.transform.position.x + fleetX - range
		    && transform.position.y <= player.transform.position.y + fleetY + range 
		    && transform.position.y >= player.transform.position.y + fleetY - range) {
			inPosition = true;
			rb2D.velocity = player.GetComponent<Follower> ().rb2D.velocity;
		}
	}


	/*
	!!!!!!!!!!!!!!!
	!!!!!!!!!!!!!!
	!!!!!!!!!!!!
	FOLLOWER MOVES
	!!!!!!!!!!!!
	!!!!!!!!!!!!!
	!!!!!!!!!!!!!!
	 */
	public void ShieldMove () {
		float pY = player.transform.position.y;
		float pX = player.transform.position.x;
		
		Vector3 target = Vector3.MoveTowards (transform.position,
		                                      new Vector3(pX + spaceX, pY + spaceY, 0),
		                                      .5f);
		if (!float.IsNaN (target.x)) {
			transform.position = target;
		}
	}
	
	public void FocusMove () {
		float pY = player.transform.position.y;
		float pX = player.transform.position.x;
		Vector3 target = Vector3.MoveTowards (transform.position,
	                                      new Vector3 (pX, pY, 0),
	                                      .5f);
		if (!float.IsNaN (target.x)) {
			transform.position = target;
		}
	}

	public void FlankMove () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		float pY = player.transform.position.y;
		float pX = player.transform.position.x;
		Vector3 target = Vector3.MoveTowards (transform.position,
		                                      new Vector3(pX + spaceX, pY - 0.50f, 0),
		                                      .5f);
		if (!float.IsNaN (target.x)) {
			transform.position = target;
		} 
	}
	
	public void BottomCornerMove(){
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		
		Vector3 target = Vector3.MoveTowards (transform.position,
		                                      new Vector3(spaceX, -4.5f, 0),
		                                      .5f);
		if (!float.IsNaN (target.x)) {
			transform.position = target;
		} 
	}

	/*
	!!!!!!!!!!!!!!!
	!!!!!!!!!!!!!!
	!!!!!!!!!!!!
	BADDY MOVES
	!!!!!!!!!!!!
	!!!!!!!!!!!!!
	!!!!!!!!!!!!!!
	 */

	public void StraightLine(){
		rb2D.velocity = new Vector2 (xSpeed, ySpeed);
	}

	public void LeftToRight(){
		if (transform.position.x > (initialX + moveBoundary) || transform.position.x > 7.5){
			goingRight = false;
		}
		
		if (transform.position.x < (initialX - moveBoundary) || transform.position.x < -7.5) {
			goingRight = true;
		}
		
		if (goingRight) {
			rb2D.AddForce(new Vector3 (xSpeed * Time.deltaTime, 0, 0));
		} else {
			rb2D.AddForce(new Vector3 (-xSpeed * Time.deltaTime, 0, 0));
		}
		
	}
	
	public void UpAndDown(){
		if (transform.position.y > (initialY + moveBoundary) || transform.position.y > 4.5){
			goingUp = false;
		}
		
		if (transform.position.y < (initialY - moveBoundary) || transform.position.y < -4.5) {
			goingUp = true;
		}
		
		if (goingUp) {
			rb2D.AddForce(new Vector3 (0, ySpeed * Time.deltaTime, 0));
		} else {
			rb2D.AddForce(new Vector3 (0, -ySpeed * Time.deltaTime, 0));
		}
	}
	
	public void SinWaveVertical() {
		timer += ((Mathf.PI/2) * Time.deltaTime);
		rb2D.velocity = new Vector3 (Mathf.Sin(timer/sinTurnFreq) * xSpeed, -ySpeed, 0);
		
	}
	
	public void SwoopMove() {
		if (!swoopingLeft) {
			timer += Time.deltaTime; 
			rb2D.velocity = new Vector3 (xSpeed + timer, ySpeed + timer, 0);
		} else {
			timer += Time.deltaTime; 
			rb2D.velocity = new Vector3 (xSpeed - timer, ySpeed + timer, 0);
		}
	}

	public void TowardsPlayer(){
		
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else {
			timer += Time.deltaTime;
		
			if (timer >= turnDelay) {
				if (player.transform.position.x > transform.position.x) {
					goingRight = true;
				}
				if (player.transform.position.x < transform.position.x) {
					goingRight = false;
				}
				timer = 0;
			}
		}
		
		if (goingRight) {
			rb2D.velocity = new Vector2 (xSpeed, -ySpeed);
		} else {
			rb2D.velocity = new Vector2 (-xSpeed, -ySpeed);
		}
		
	}

	public void setDestinationXY(float x, float y){
		destinationX = x;
		destinationY = y;
	}

	public void MoveShootMove() {
		if (!inPosition) {
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
				shotDelay /= 2;
			}
		} else {
			timer += Time.deltaTime;
			if (timer >= pauseDelay){
				shotDelay *= 2;
				destinationX = -destinationX;
				inPosition = false;
				timer = 0;
			}
		}
		
	}

	/*
	!!!!!!!!!!!!!!!
	!!!!!!!!!!!!!!
	!!!!!!!!!!!!
	PROJECTILE MOVES
	!!!!!!!!!!!!
	!!!!!!!!!!!!!
	!!!!!!!!!!!!!!
	 */

	public void ShotMove(){
		rb2D.velocity = new Vector2 (0, ySpeed * Time.deltaTime);
	}

	public void badMissileMove(){
		transform.up = rb2D.velocity;

		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		} else {
			float pY = player.transform.position.y;
			float pX = player.transform.position.x;
			float fX = transform.position.x;
			float fY = transform.position.y;

			rb2D.AddForce (new Vector2 ((pX - fX) * xSpeed, (pY - fY) * xSpeed));
		}

	}

	public void goodMissileMove(){
		transform.up = rb2D.velocity;
		if (baddy == null || 
		    baddy.transform.position.x < -10 || 
		    baddy.transform.position.x > 10 || 
		    baddy.transform.position.y < -6 || 
		    baddy.transform.position.y > 6){
			rb2D.AddForce(new Vector2(0, 1));
			return;
		}
		float bY = baddy.transform.position.y;
		float bX = baddy.transform.position.x;
		float fX = transform.position.x;
		float fY = transform.position.y;
		
		rb2D.AddForce (new Vector2 ((bX - fX) * xSpeed, (bY - fY) * xSpeed));
		}
}
