using UnityEngine;
using System.Collections;

public class VirtualObjectMap : MonoBehaviour {

	//Enum of posible color of the Objet
	public enum ObjectColor
	{ 
		Red,
		Blue,
		Yellow,
		Magenta,
		Green
	}

	//PARAMETER OF THE OBJET

	//Objet resource of my position
	private GpsWithoutUi gps;
	private bool Click = false;	

	//Describe parameter of the objet
	[SerializeField]
	private double latitude;	
	[SerializeField]
	private double longitude;	
	[SerializeField]
	private string description;	
	[SerializeField]
	private ObjectColor color;
	[SerializeField]
	private int type;	

	private float dist;	
	private string Nombre;


	// Function that calculate the distance
	private float CalcDistance(float lon1, float lat1, float lon2, float lat2){
		int R = 6371; // radius of earth in km
		float dLat = (lat2-lat1)*(Mathf.PI / 180);
		float dLon = (lon2-lon1)*(Mathf.PI / 180);
		lat1 = lat1*(Mathf.PI / 180);
		lat2 = lat2*(Mathf.PI / 180);
		float a = Mathf.Sin(dLat/2) * Mathf.Sin(dLat/2) + Mathf.Sin(dLon/2) * Mathf.Sin(dLon/2) * Mathf.Cos(lat1) * Mathf.Cos(lat2); 
		float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1-a)); 
		float d = R * c;
		return d; //distance in kms
	}

	//Objet events and properties change function
	private void ShowMe(){
		gameObject.GetComponent<MeshRenderer>().enabled = true;
	}	
	private void HideMe(){
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}	
	//Change click to show distance or name
	void OnMouseDown(){
		Click = !Click;
	}

	public void SetLatLon(){ //assign the position of the gps, in this example, we assign the center of the map position
		
		latitude = PositionUtilities.Latitude;
		longitude = PositionUtilities.Longitude;
		
	
		latitude = 41.275626f;
		longitude = 1.986510f;
		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);

		gameObject.transform.position = new Vector3(x, z, 0);
	}


	public void SetDesc(string s){
		description = s;
			gameObject.GetComponentInChildren<TextMesh>().text = s;
	}

	// Use this for initialization of the Objet
	void Start () 
	{
		dist =0;

		//Assing color to the objet
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
			case ObjectColor.Magenta:
				gameObject.GetComponent<MeshRenderer>().material.color = Color.magenta;
				break;

			default:
				break;
		}		

		//Put description on text from the objet
		gameObject.GetComponentInChildren<TextMesh>().text = description;		
		Nombre = gameObject.GetComponentInChildren<TextMesh> ().text;
		
		PositionUtilities.Init();
		//Put position of the objet in the map using PositionUtilities class to get x,z position formo lat and long
		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);
		// Put the objet in the coordinate with 0 elevation
		gameObject.transform.position = new Vector3(x, z, 0);
		
		//Calculate distance object - device
		dist = CalcDistance((float) PositionUtilities.Longitude, (float)PositionUtilities.Latitude,(float)longitude, (float)latitude);
		//if the distance is lower that 800m show me the objet
		if (dist < 0.800f){
			ShowMe();
		}else{
			HideMe();
		}				
	}

	void Awake()
	{
		gps = GameObject.FindGameObjectWithTag("Gps").GetComponent<GpsWithoutUi>();
	}

	void Update(){

		dist = CalcDistance((float)PositionUtilities.Longitude,(float) PositionUtilities.Latitude,(float)longitude, (float)latitude);
		//Debug.Log ("Dist "+ dist);

		if (dist < 0.800f){
			ShowMe();
		}else{
			HideMe();
		}

		if (Click == true) {
			//Show distance
			gameObject.GetComponentInChildren<TextMesh>().text = "Distance: " + dist.ToString() + " km";
		} else {
			//Show original description
			gameObject.GetComponentInChildren<TextMesh>().text = Nombre;
		}
	}

	//What is that?? ANA??
	//Repintar
	public void ReMap(){
		PositionUtilities.Init();
		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);		
		gameObject.transform.position = new Vector3(x, z, 0);
	}

}
