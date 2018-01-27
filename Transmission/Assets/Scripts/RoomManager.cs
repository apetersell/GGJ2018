using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public Vector2[] coinPositions;
	public Vector2[] spikePositions;
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
	public Color completedColor;
    public CameraMultiTargetObjective camTarg;
	CameraMultitarget cam; 
	bool badRoom;


	// Use this for initialization
	void Start () 
	{
        //sc = GameObject.Find("Main Camera").GetComponent< SmashCamScript > ();
        timeUnitlCoin = Random.Range (minCoinTime, maxCoinTime);
		if (badRoom)
		{
			int rando = Random.Range (0, coinPositions.Length);
			GameObject spikeball = Instantiate (Resources.Load ("Prefabs/Spikeball")) as GameObject;
			spikeball.transform.SetParent (transform);
			spikeball.transform.localPosition = spikePositions [rando];
			cover.GetComponent<SpriteRenderer> ().color = Color.red;
		}
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
		if (active) 
		{
			currentCoinTimer += Time.deltaTime;
			Vector3 circleScale = new Vector3 (currentScale, currentScale, currentScale); 
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
			circle.GetComponent<SpriteRenderer> ().color = completedColor;
			if (completed == false) 
			{
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
		roomScore++;
	}

	public void makeCoin()
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

	public void activate ()
	{
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
		int rando = Random.Range (0, 10);
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
}
