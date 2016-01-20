using UnityEngine;
using System.Collections;

public class VirtualObject : MonoBehaviour
{


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

private float distance;

private GpsWithoutUi MyPosition;
// Use this for initialization
void Start () 
{
	
	
	MyPosition = GameObject.FindGameObjectWithTag("Gps").GetComponent<GpsWithoutUi>();
	
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
	
	//Aqui voy a poner la distancia en lugar del nombre
	gameObject.GetComponentInChildren<TextMesh>().text = description;
	
	float z = (float)PositionUtilities.DrawCubeY(latitude);
	float x = (float)PositionUtilities.DrawCubeX(longitude);
	
	gameObject.transform.position = new Vector3(x, 0, z);
	
	
}


float CalcDistance(float lon1, float lat1, float lon2, float lat2){
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


void OnMouseDown(){
	gameObject.GetComponent<MeshRenderer>().enabled = false;
	
}
// Update is called once per frame
void Update () {
	
	float distance = CalcDistance((float)longitude,(float)latitude,(float)MyPosition.Longitude,(float)MyPosition.Latitude);
	//this.description = "Distance To the Object: "+ distance + " m";
	this.description = distance.ToString() + "km";
	//Aqui voy a poner la distancia en lugar del nombre
	gameObject.GetComponentInChildren<TextMesh>().text = description;
	
}

}