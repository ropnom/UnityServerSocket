using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {
	
	public enum ObjectColor
	{ 
		Red,
		Blue,
		Yellow,
		Green
	}
	
	[SerializeField]
	private double latitude;
	
	[SerializeField]
	private double longitude;
	
	[SerializeField]
	private string description;
	
	[SerializeField]
	private ObjectColor color;
	
	// Use this for initialization
	void Start () 
	{
		switch (color)
		{
		case ObjectColor.Red:
			gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
			break;
		case ObjectColor.Blue:
			gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
			break;
		case ObjectColor.Green:
			gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
			break;
		case ObjectColor.Yellow:
			gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
			break;
		default:
			break;
		}
		
		
		gameObject.GetComponentInChildren<TextMesh>().text = description;
		
		
		//centerMapLat = "41.25872648";
		//centerMapLon = "1.92101612";
		
		//Point p = PositionUtilities.WorldToTilePos(longitude, latitude);
		//PositionUtilities.TileX = Mathf.FloorToInt((float)p.X);
		//PositionUtilities.TileY = Mathf.FloorToInt((float)p.Y);
		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);
		
		gameObject.transform.position = new Vector3(x, z, 0);
	}
	
	void OnMouseDown(){
		//gameObject.GetComponent<MeshRenderer>().enabled = false;
		
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLatLon(){ //assign the position of the gps, in this example, we assign the center of the map position

		latitude = PositionUtilities.Latitude;
		longitude = PositionUtilities.Longitude;

		
		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);
		
		gameObject.transform.position = new Vector3(x, z, 0);
	}

	public void SetDesc(string s){
		description = s;
		gameObject.GetComponentInChildren<TextMesh>().text = s;

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
