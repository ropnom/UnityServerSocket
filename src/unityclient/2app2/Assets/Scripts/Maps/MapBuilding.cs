using UnityEngine;
using System.Collections;

public class MapBuilding : MonoBehaviour {

	GameObject[] o;
	GameObject o2;
	GameObject[] o3;
	VirtualObjectMap vo;
	//VirtualObjectMap2 vo1;
	userLocation vo2;
	VirtualObjectMap2 vo3;


	void Awake()
	{
		o = GameObject.FindGameObjectsWithTag("Poi");
		o2 = GameObject.Find ("User");
		o3 = GameObject.FindGameObjectsWithTag("Dinamico");

	}

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update() {
	   
	}
	public void Zommin(){
	  
		PositionUtilities.Zoom++;
		if(PositionUtilities.Zoom==19)
			PositionUtilities.Zoom=18;

		foreach ( GameObject i in o){
			vo = i.GetComponent<VirtualObjectMap>();
			vo.ReMap();
			//vo1=i.GetComponent<VirtualObjectMap2>();
			//vo1.ReMap();
			//vo2=i.GetComponent<userLocation>();
			//vo2.ReMap();
			//vo3=i.GetComponent<CubeScript>();
			//vo3.ReMap();
		}

		foreach ( GameObject j in o3){
			vo3 = j.GetComponent<VirtualObjectMap2>();
			vo3.ReMap();
			//vo1=i.GetComponent<VirtualObjectMap2>();
			//vo1.ReMap();
			//vo2=i.GetComponent<userLocation>();
			//vo2.ReMap();
			//vo3=i.GetComponent<CubeScript>();
			//vo3.ReMap();
		}

		vo2 = o2.GetComponent<userLocation> ();
		vo2.ReMap();

	}

	public void Zommout(){
		
		PositionUtilities.Zoom--;
		if(PositionUtilities.Zoom<14)
			PositionUtilities.Zoom=14;

		foreach ( GameObject i in o){
			vo = i.GetComponent<VirtualObjectMap>();
			vo.ReMap();
			//vo1=i.GetComponent<VirtualObjectMap2>();
			//vo1.ReMap();
			//vo2=i.GetComponent<userLocation>();
			//vo2.ReMap();
			//vo3=i.GetComponent<CubeScript>();
			//vo3.ReMap();

		}

		foreach ( GameObject j in o3){
			vo3 = j.GetComponent<VirtualObjectMap2>();
			vo3.ReMap();
			//vo1=i.GetComponent<VirtualObjectMap2>();
			//vo1.ReMap();
			//vo2=i.GetComponent<userLocation>();
			//vo2.ReMap();
			//vo3=i.GetComponent<CubeScript>();
			//vo3.ReMap();
		}

		vo2 = o2.GetComponent<userLocation> ();
		vo2.ReMap();

	}


}
