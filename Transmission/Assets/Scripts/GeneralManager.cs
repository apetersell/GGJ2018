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

	// Use this for initialization
	void Start () 
	{
		score = 1;
		int rando = Random.Range (0, compass.Length);
		direction = compass [rando];
        Debug.Log(direction);
		goalValueMinus = -goalValuePlus;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (direction == "North" || direction == "East") {
			goalDistance = goalValuePlus;
		} else {
			goalDistance = goalValueMinus; 
		}
	}

	public static void takeDamage ()
	{
		score--;
		foreach (var guy in players) 
		{
			guy.hit = true;
		}
	}
	public static void scorePoints()
	{
		score++;
	}
}
