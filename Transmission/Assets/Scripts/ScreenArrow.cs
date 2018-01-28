using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenArrow : MonoBehaviour {

	public Sprite[] arrows;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GeneralManager.direction == "North") 
		{
			GetComponent<SpriteRenderer> ().sprite = arrows [0];
		}

		if (GeneralManager.direction == "East") 
		{
			GetComponent<SpriteRenderer> ().sprite = arrows [1];
		}

		if (GeneralManager.direction == "South") 
		{
			GetComponent<SpriteRenderer> ().sprite = arrows [2];
		}

		if (GeneralManager.direction == "West") 
		{
			GetComponent<SpriteRenderer> ().sprite = arrows [3];
		}
	}
}
