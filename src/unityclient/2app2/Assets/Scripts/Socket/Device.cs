using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {

	protected int identification;
	protected int type;
	protected double latitude;
	protected double longitude;
	protected string Description;
	protected int position;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GenMessage() {
		string message = this.identification + ":" + this.type + ":" + this.latitude + ":" + this.longitude + ":" + this.Description;
		return message;
	}


	//**************************************
	// GETs and SETs

	public int getType() {
		return type;
	}
	
	public void setType(int type) {
		this.type = type;
	}
	
	public string getDescription() {
		return Description;
	}
	
	public void setDescription(string description) {
		Description = description;
	}
	
	public double getLatitude() {
		return latitude;
	}
	
	public void setLatitude(double latitude) {
		this.latitude = latitude;
	}
	
	public double getLongitude() {
		return longitude;
	}
	
	public void setLongitude(double longitude) {
		this.longitude = longitude;
		
	}
	
	public int getIdentification() {
		return identification;
	}
	
	public void setIdentification(int identification) {
		this.identification = identification;
	}
	
	public int getPosition() {
		return position;
	}
	
	public void setPosition(int position) {
		this.position = position;
	}
}
