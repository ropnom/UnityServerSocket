using UnityEngine;
using System.Collections;

public class UserScript : MonoBehaviour {

	GpsWithoutUi gps;

	
	void Awake()
	{
		gps = GameObject.FindGameObjectWithTag("Gps").GetComponent<GpsWithoutUi>();

	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log("Gps value "+gps.Longitude);
		if (gps.Latitude != 0.0f)
		{

			PositionUtilities.Init();
			PositionUtilities.Zoom=16;
			float y = (float)PositionUtilities.DrawCubeY((double)gps.Latitude);
			float x = (float)PositionUtilities.DrawCubeX((double)gps.Longitude);
			gameObject.transform.position = new Vector3(x, y, 0);
			
		}
		

	}
}
