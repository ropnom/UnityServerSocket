using UnityEngine;
using System.Collections;

public class Over : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseOver(){
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
}
