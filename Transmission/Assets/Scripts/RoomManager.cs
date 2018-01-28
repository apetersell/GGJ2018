using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public Vector2[] coinPositions;
	public Vector2[] spikePositions;
	public Color[] circleColors;
	public float roomScore;
	public float roomScoreMax;
	public float timeUnitlCoin;
	public float currentCoinTimer;
	public float maxCoinTime;
	public float minCoinTime;
	public float roomDistance;
	public GameObject coin;
	public bool first;
	public bool active;
	public bool completed;
	public GameObject cover;
	public List<RoomManager> connectedRooms = new List<RoomManager> ();
	public GameObject circle;
	public float expandedScale;
	float currentScale;
    public CameraMultiTargetObjective camTarg;
	CameraMultitarget cam; 
	public bool badRoom;
	public SpriteRenderer interior;
	public SpriteRenderer exterior;
	public Sprite badHouseInt;
	public Sprite badHousExt;
	public Vector3 circleScale;
	public ParticleSystem ps;
    public CrossfadeOnButton cfade;
    public static bool ending;


	// Use this for initialization
	void Start () 
	{
		if (badRoom) 
		{
			interior.sprite = badHouseInt;
			exterior.sprite = badHousExt;
		}
        //sc = GameObject.Find("Main Camera").GetComponent< SmashCamScript > ();
        timeUnitlCoin = Random.Range (minCoinTime, maxCoinTime);
		if (first) 
		{
			GeneralManager.roomPositions.Add (transform.position);
            //sc.Players.Add((camTarget));
			if (active) 
			{
				Camera.main.GetComponent<CameraMultitarget>().startZoom = Camera.main.GetComponent<CameraMultitarget>().endZoom;
				Camera.main.GetComponent<CameraMultitarget>().StartCoroutine(Camera.main.GetComponent<CameraMultitarget>().ZoomOut()); 
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		circleColor (roomScore);
		if (active) 
		{
			currentCoinTimer += Time.deltaTime;
			circleScale = new Vector3 (currentScale, currentScale, currentScale); 
			circle.transform.localScale = circleScale;
			currentScale = expandedScale * (roomScore / roomScoreMax);
		}

		if (active) {
			cover.SetActive (false);
			circle.SetActive (true);
		} else {
			cover.SetActive (true);
			circle.SetActive (false);
		}

		if (currentCoinTimer >= timeUnitlCoin) 
		{
			if (coin == null) 
			{
				makeCoin ();
			}
		}

		if (roomScore >=roomScoreMax) 
		{
			roomScore = roomScoreMax;
			if (completed == false) 
			{
                //if main music isn't playing, switch to main music
                //cfade.TrackSwitch();
				completed = true;
				foreach (var room in connectedRooms) 
				{
					room.activate ();
				}
			}
		}
	}

	public void scorePoints ()
	{
		if (roomScore < roomScoreMax) 
		{
			roomScore++;
		}

	}

	public void makeCoin()
	{
		if (roomScore < roomScoreMax) 
		{
			currentCoinTimer = 0;
			GameObject c = Instantiate (Resources.Load ("Prefabs/Coin")) as GameObject;
			int rando = Random.Range (0, coinPositions.Length);
			coin = c;
			c.transform.SetParent (transform);
			c.transform.localPosition = coinPositions [rando];
			float newCoinTime = Random.Range (minCoinTime, maxCoinTime);
			timeUnitlCoin = newCoinTime;
		}
	}

	public void activate ()
	{
		GeneralManager.scorePoints (); 

        if (!ending)
		{
            CompassGenerator();
        }
		if (badRoom) 
		{
			if (badRoom)
			{
				int rando = Random.Range (0, spikePositions.Length);
				GameObject spikeball = Instantiate (Resources.Load ("Prefabs/Spikeball")) as GameObject;
				spikeball.transform.SetParent (transform);
				spikeball.transform.localPosition = spikePositions [rando];
				spikeball.GetComponent<Spikes> ().pos = rando;
				GeneralManager.updateSpikeVals (rando);
			}
		}
		active = true;
        camTarg.enabled = true;
		for (int i = 0; i < 4; i++) 
		{
			if (i == 0) 
			{
				Vector2 newRoomPos = new Vector2 (transform.position.x + roomDistance, transform.position.y + roomDistance);
				if (!GeneralManager.roomPositions.Contains (newRoomPos)) 
				{
					makeNewRoom (newRoomPos);
				}
			}

			if (i == 1) 
			{
				Vector2 newRoomPos = new Vector2 (transform.position.x + roomDistance, transform.position.y - roomDistance);
				if (!GeneralManager.roomPositions.Contains (newRoomPos)) 
				{
					makeNewRoom (newRoomPos);
				}
			}

			if (i == 2) 
			{
				Vector2 newRoomPos = new Vector2 (transform.position.x - roomDistance, transform.position.y - roomDistance);
				if (!GeneralManager.roomPositions.Contains (newRoomPos)) 
				{
					makeNewRoom (newRoomPos);
				}
			}

			if (i == 3) 
			{
				Vector2 newRoomPos = new Vector2 (transform.position.x - roomDistance, transform.position.y + roomDistance);
				if (!GeneralManager.roomPositions.Contains (newRoomPos)) 
				{
					makeNewRoom (newRoomPos);
				}
			}
		}
	}

	void makeNewRoom (Vector2 pos)
	{
		int rando = Random.Range (0, 6);
		GameObject roomy = Instantiate(Resources.Load("Prefabs/Room")) as GameObject; 
		roomy.transform.position = pos;
		connectedRooms.Add (roomy.GetComponent<RoomManager>());
		roomy.GetComponent<RoomManager> ().first = false;
		if (rando == 5) 
		{
			roomy.GetComponent<RoomManager> ().badRoom = true;
		}
		GeneralManager.roomPositions.Add (pos);
        Camera.main.GetComponent<CameraMultitarget>().startZoom = Camera.main.GetComponent<CameraMultitarget>().endZoom;
        Camera.main.GetComponent<CameraMultitarget>().StartCoroutine(Camera.main.GetComponent<CameraMultitarget>().ZoomOut()); 
	}

	void circleColor (float value)
	{
		if (value == 1) 
		{
			ps.startColor = circleColors [0];
		}

		if (value == 2) 
		{
			ps.startColor = circleColors [1];
		}

		if (value == 3) 
		{
			ps.startColor = circleColors [2];
		}

		if (value == 4) 
		{
			ps.startColor = circleColors [3];
		}

		if (value == 5) 
		{
			ps.startColor = circleColors [4];
		}

	}

    public void CompassGenerator ()
    {
        float dir = GeneralManager.goalDistance;

        if (GeneralManager.direction == "North")
        {
            if (gameObject.transform.position.y > dir && dir > 0)
            {
                Debug.Log("I WIN CUZ NORTH");
                ending = true;
            }
        }
        if (GeneralManager.direction == "South")
        {
            if (gameObject.transform.position.y < dir && dir < 0)
            {
                Debug.Log("I WIN CUZ SOUTH");
                ending = true;
            } 
        }

        if (GeneralManager.direction == "East")
        {
            if (gameObject.transform.position.x > dir && dir > 0)
            {
                Debug.Log("I WIN CUZ EAST");
                ending = true;
            }
        }

        if (GeneralManager.direction == "West")
        {
            if (gameObject.transform.position.x < dir && dir < 0)
            {
                Debug.Log("I WIN CUZ WEST");
                ending = true;
            }
        }
    }
    //check end goal
    //check your position

}