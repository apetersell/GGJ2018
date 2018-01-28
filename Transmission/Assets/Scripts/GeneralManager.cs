using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour 
{
	public static List<Vector2> roomPositions = new List<Vector2> ();
	public static List<PlayerController> players = new List<PlayerController> ();
	public static float score;
	float maxHealth = 100;

	public string[] compass;
	public static string direction;
	public float goalValuePlus;
	float goalValueMinus;
	public static float goalDistance;
	public static int spikesInPos0;
	public static int spikesInPos1;
	public static float damageMulit;
	bool ending = false;

	void Awake ()
	{
		roomPositions.Clear ();
		players.Clear ();
		score = 1;
		spikesInPos0 = 0;
		spikesInPos1 = 0;
		damageMulit = 1;
		ending = false;
	}

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Score " + score);
		Debug.Log ("Spikes1 " + spikesInPos1);
		Debug.Log ("Spikes2 " + spikesInPos1);
		Debug.Log ("damageMulti" + damageMulit);
		score = 1;
		int rando = Random.Range (0, compass.Length);
		direction = compass [rando];
        Debug.Log(direction);
		goalValueMinus = goalValuePlus * -1;
	}
	
	// Update is called once per frame
	 void Update () 
	{ 
		Debug.Log ("Score: " + score);
		damageMulit = GameObject.Find ("Score").GetComponent<ScoreDisplay> ().damageValue;
		if (direction == "North" || direction == "East") {
			goalDistance = goalValuePlus;
		} else {
			goalDistance = goalValueMinus; 
		} 

		if (score <= 0) 
		{
			if (!ending) 
			{
				ending = true;
				FadeEffect fe = GameObject.Find ("Veil").GetComponent<FadeEffect> ();
				fe.nextScene = 3;
				fe.active = true;
				StartCoroutine (fe.fadeOut ());
			}
		}
	}

	public static void takeDamage (int sent)
	{
		if (sent == 0) 
		{
			score -= spikesInPos0 * damageMulit;
		}
		if (sent == 1) 
		{
			score -= spikesInPos1 * damageMulit; 
		}
		foreach (var guy in players) 
		{
			guy.hit = true;
		}
	}
	public static void scorePoints()
	{
		score++;
	}

	public static void updateSpikeVals (int sent)
	{
		if (sent == 0) {
			spikesInPos0++;
		} else {
			spikesInPos1++;
		}
	}

	public static void reset ()
	{
		score = 0;
		roomPositions.Clear ();
		players.Clear ();
		SceneManager.LoadScene (0);
	}


}
