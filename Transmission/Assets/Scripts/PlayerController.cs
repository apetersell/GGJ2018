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
	public bool hit;
	public float hitTimerMax;
	float hitTimer;
	Color flashing;
	public float colorLerpSpeed;
	public RuntimeAnimatorController[] controllers;
	public Sprite[] sprites;
    public AudioClip[] sfx;
    public AudioSource playerAud;
    public AudioSource hitAud;


	// Use this for initialization
	void Start () 
	{
		int rando = Random.Range (0, sprites.Length);
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		GeneralManager.players.Add (this);
		sr.sprite = sprites [rando];
		anim.runtimeAnimatorController = controllers [rando];
	}
	
	// Update is called once per frame
	void Update () 
	{
		flashing = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time*colorLerpSpeed, 1));
		animations ();
		movement ();
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) 
		{
			if (canJump) 
			{
                playerAud.PlayOneShot(sfx[0]);
				jump ();
			}
		}

		if (hit) {
			hitTimer += Time.deltaTime;
			sr.color = flashing;
		} else {
			sr.color = Color.white;
		}

		if (hitTimer >= hitTimerMax) 
		{
			hit = false;
			hitTimer = 0;
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
            hitAud.PlayOneShot(sfx[1]);
			rm.scorePoints();
			Destroy (coll.gameObject);
		}
		if (coll.gameObject.tag == "Danger") 
		{
			if (!hit) 
			{
                hitAud.PlayOneShot(sfx[2]);
				StartCoroutine(GameObject.Find ("GeneralManager").GetComponent<ScreenFlash> ().screenFlash ());
				int whichSpike = coll.GetComponent<Spikes> ().pos;
				GeneralManager.takeDamage (whichSpike);
			}
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
