using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
	public bool canJump;
	Rigidbody2D rb;
	SpriteRenderer sr;
	public RoomManager rm;
	Animator anim;
	bool running;
	bool hit;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		animations ();
		movement ();
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) 
		{
			if (canJump) 
			{
				jump ();
			}
		}
	}

	void movement ()
	{
		if (Input.GetAxis ("Horizontal") > 0) {
			sr.flipX = false;
			rb.velocity = new Vector2 (speed, rb.velocity.y);
		} else if (Input.GetAxis ("Horizontal") < 0) {
			sr.flipX = true;
			rb.velocity = new Vector2 (speed * -1, rb.velocity.y);
		} else {
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}

	}

	void jump ()
	{
		canJump = false;
		rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed); 
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Floor") 
		{
			canJump = true;
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Coin") 
		{
			rm.scorePoints();
			Destroy (coll.gameObject);
		}
		if (coll.gameObject.tag == "Danger") 
		{

		}
	}

	void animations()
	{
		anim.SetBool ("Touching Ground", canJump);
		anim.SetFloat ("Speed X", rb.velocity.x);
		anim.SetFloat ("Speed Y", rb.velocity.y);
		anim.SetBool ("Running", running);

		if (rb.velocity.x != 0 && canJump) {
			running = true;
		} else {
			running = false;
		}
	}
}
