using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () 
	{
		score = 1;
		int rando = Random.Range (0, compass.Length);
		direction = compass [rando];
        Debug.Log(direction);
		goalValueMinus = goalValuePlus * -1;
	}
	
	// Update is called once per frame
	 void Update () 
	{ 
		damageMulit = GameObject.Find ("Score").GetComponent<ScoreDisplay> ().damageValue;
		if (direction == "North" || direction == "East") {
			goalDistance = goalValuePlus;
		} else {
			goalDistance = goalValueMinus; 
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


}
