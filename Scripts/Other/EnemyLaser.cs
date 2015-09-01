using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {
	public LayerMask mask;
	
	public LineRenderer laser;
	public BoxCollider2D boxCol;
	public bool active;
	public bool aiming;
	public bool firing;
	public float aimTimer;
	public float aimMax;
	public float firingTime;
	public float fireMax;
	
	// Use this for initialization
	void Start () {
		laser = GetComponent<LineRenderer> ();
		boxCol = GetComponent<BoxCollider2D> ();
	}
	
	void Update () {
		if (active) {
			laser.SetPosition (0, transform.position);
			laser.SetPosition (1, new Vector2 (transform.position.x, -20));
			if (aiming) {
				aimTimer += Time.deltaTime;
				if (aimTimer >= aimMax) {
					aimTimer = 0;
					firing = true;
					aiming = false;
					laser.SetWidth(0.1f, 0.1f);
				}
			} else if (firing) {
				firingTime += Time.deltaTime;
				RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 20, mask);
				if (hit.collider != null && (hit.collider.tag.Equals("Follower") || hit.collider.tag.Equals("Player"))){
					laser.SetPosition (1, new Vector2(transform.position.x, hit.collider.transform.position.y));
					hit.collider.GetComponent<Follower>().TakeDamage(Time.deltaTime * 2);
				}
				if (firingTime >= fireMax) {
					laser.SetPosition (0, new Vector2 (0, 0));
					laser.SetPosition (1, new Vector2 (0, 0));
					laser.SetWidth(0.02f, 0.02f);
					firingTime = 0;
					firing = false;
					active = false;
				}
			}
		}
	}
	
	// Update is called once per frame
	public void Fire(){
		if (!active) {
			active = true;
			aiming = true;
		}
	}

}
