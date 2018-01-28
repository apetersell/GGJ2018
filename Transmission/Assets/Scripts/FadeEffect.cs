using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour {


	public float fadeDuration;
	public int nextScene;
	public bool fadeInAtStart;
	public bool active;
	Color displayColor;
	Image img;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
		if (fadeInAtStart) 
		{
			StartCoroutine (fadeIn());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (active) {
			img.color = displayColor;
		} else {
			img.color = Color.clear;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine (fadeOut());
		}
	}

	public IEnumerator fadeIn ()
	{
		float currentTime = 0;
		while (currentTime < fadeDuration) 
		{
			displayColor = Color.Lerp (Color.black, Color.clear, currentTime / fadeDuration);
			currentTime += Time.deltaTime;
			yield return null;
		}
	}

	public IEnumerator fadeOut ()
	{
		float currentTime = 0;
		while (currentTime < fadeDuration) 
		{
			displayColor = Color.Lerp (Color.clear, Color.black, currentTime / fadeDuration);
			currentTime += Time.deltaTime;
			yield return null;
		}

		SceneManager.LoadScene (nextScene);
	}
}
