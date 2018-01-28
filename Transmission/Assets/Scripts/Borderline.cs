using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borderline : MonoBehaviour 
{
	public Color lerpingColor;
	public Color dark;
	public Color bright;
	public Color unselected;
	// Use this for initialization

	public string myDir;
	public static float colorLerpSpeed = 1;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		lerpingColor = Color.Lerp(dark, bright, Mathf.PingPong(Time.time*colorLerpSpeed, 1));

		if (myDir == GeneralManager.direction) {
			GetComponent<SpriteRenderer> ().color = lerpingColor; 
		} else {
			GetComponent<SpriteRenderer> ().color = unselected;
		}
	}
}
