﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	Image display;
	public float [] pointThresholds;
	public Sprite [] sprites;
    public CrossfadeOnButton crossFade;
	public int damageValue;

	// Use this for initialization
	void Start () 
	{
		display = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GeneralManager.score < pointThresholds [0]) 
		{
			display.sprite = sprites [0];
			damageValue = 1;
		}

		if (GeneralManager.score < pointThresholds [1] && GeneralManager.score >= pointThresholds [0])  
		{
			display.sprite = sprites [1];
			damageValue = 1;
		}

		if (GeneralManager.score < pointThresholds [2] && GeneralManager.score >= pointThresholds [1])  
		{
			display.sprite = sprites [2];
			damageValue = 2;
		}

		if (GeneralManager.score < pointThresholds [3] && GeneralManager.score >= pointThresholds [3])  
		{
			display.sprite = sprites [3];
            crossFade.TrackSwitch();
			damageValue = 2;
		}

		if (GeneralManager.score >= pointThresholds [3])  
		{
			display.sprite = sprites [4];
			damageValue = 3;
		}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    crossFade.TrackSwitch();
                     
        //}
	}


}
