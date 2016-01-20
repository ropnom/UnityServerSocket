using UnityEngine;
using System.Collections;

public class userLocation : MonoBehaviour {

	GpsWithoutUi gps;

	[SerializeField]
	private double latitude;	
	[SerializeField]
	private double longitude;
	
	void Awake()
	{
		gps = GameObject.FindGameObjectWithTag("Gps").GetComponent<GpsWithoutUi>();
	}
	// Use this for initialization
	void Start () {
	
		PositionUtilities.Init();
		//PositionUtilities.Zoom=15;
		float y = (float)PositionUtilities.DrawCubeY((double)41.275264);
		float x = (float)PositionUtilities.DrawCubeX((double)1.98729);

		
		gameObject.transform.position = new Vector3(x, y, 0);
	}
	
	// Update is called once per frame
	void Update () {
	

		if (gps.Latitude != 0.0f)
		{
			PositionUtilities.Init();
			//PositionUtilities.Zoom=15;

			float y = (float)PositionUtilities.DrawCubeY((double)gps.Latitude);
			float x = (float)PositionUtilities.DrawCubeX((double)gps.Longitude);

			gameObject.transform.position = new Vector3(x, y, 0);
			
		}
	}
	public void ReMap(){

		PositionUtilities.Init();

		latitude = PositionUtilities.Latitude;
		longitude = PositionUtilities.Longitude;

		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);		
		gameObject.transform.position = new Vector3(x, z, 0);
	}
}
