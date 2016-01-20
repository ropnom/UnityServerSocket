using UnityEngine;
using System.Collections;

public class SetLoc : MonoBehaviour {


	public void CastelldefelsCenterMap(){
		PositionUtilities.Latitude = 41.275264;
		PositionUtilities.Longitude= 1.98729;
		PositionUtilities.centerMapLat= "41.275264";
		PositionUtilities.centerMapLon= "1.98729";
	}

	public void VilaOlimpicaCenterMap(){
		PositionUtilities.Latitude = 41.389310;
		PositionUtilities.Longitude= 2.194775;
		PositionUtilities.centerMapLat= "41.389310";
		PositionUtilities.centerMapLon= "2.194775";
	}

	public void GpsCenterMap(){
		//PositionUtilities.Latitude = 
		//PositionUtilities.Longitude=
		//PositionUtilities.centerMapLat= 
		//PositionUtilities.centerMapLon=
	}
}
