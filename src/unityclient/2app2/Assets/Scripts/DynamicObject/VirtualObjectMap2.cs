using UnityEngine;
using System.Collections;

public class VirtualObjectMap2 : MonoBehaviour {

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
	private int type;	

	private float dist;	
	private string Nombre;
	private int position;


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
		
	
		latitude = 41.2752f;
		longitude = 1.982f;
		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);

		gameObject.transform.position = new Vector3(x, z, 0);
	}


	public void SetDesc(string s){
		description = s;
			gameObject.GetComponentInChildren<TextMesh>().text = s;
	}


	public void ContructVirtualObjectMap2(int type2, double latitude2, double longitude2, string description2, int position2){
		this.latitude = latitude2;
		this.longitude = longitude2;
		this.type = type2;
		this.description = description2;
		this.position = position2;
		//
		/*description = description2;

		latitude = 41.2752f;
		longitude = 1.982f;*/
		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);

		gameObject.transform.position = new Vector3(x, z, 0);
		gameObject.GetComponentInChildren<TextMesh>().text = description;


		//Start ();
	}

	// Use this for initialization of the Objet
	void Start () 
	{
		position = -1;
		dist =0;

		//Assing color to the objet
		switch (type)
		{
			case 0:
				gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
				break;
			case 1:
				gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
				break;
			case 2:
				gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
				break;
			case 3:
				gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
				break;
			case 4:
				gameObject.GetComponent<MeshRenderer>().material.color = Color.magenta;
				break;

			default:
				break;
		}		

		//Put description on text from the objet
		gameObject.GetComponentInChildren<TextMesh>().text = description;		
		
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
			gameObject.GetComponentInChildren<TextMesh>().text = description;
		}
		ReMap ();
	}

	//Repintar
	public void ReMap(){
		PositionUtilities.Init();

		//latitude = PositionUtilities.Latitude;
		//longitude = PositionUtilities.Longitude;

		Device a = SocketClient.getDevice (position);
		//para evitar null cuando hace el update antes qeu ciertos funciones...
		if (a != null) {
			latitude = a.getLatitude ();
			longitude = a.getLongitude ();
		}

		float z = (float)PositionUtilities.DrawCubeY(latitude);
		float x = (float)PositionUtilities.DrawCubeX(longitude);		
		gameObject.transform.position = new Vector3(x, z, 0);
	}

}
