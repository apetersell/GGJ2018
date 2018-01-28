using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour {

	public float hitflashDuration;
	public Color displayColor;
	public Color neutralBG;

	// Use this for initialization
	void Start () 
	{
		displayColor = neutralBG;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Camera.main.backgroundColor = displayColor;
	}

	public IEnumerator screenFlash ()
	{
		float currentTime = 0;
		while (currentTime < hitflashDuration) 
		{
			displayColor = Color.Lerp (Color.red, neutralBG, currentTime / hitflashDuration);
			currentTime += Time.fixedDeltaTime;
			yield return null;
		}
	}
}
