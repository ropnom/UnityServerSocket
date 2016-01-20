using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {


	private int up;
	private int down;
	private int left;
	private int right;
	
	private float yy;
	private float xx;


	// Use this for initialization
	void Start () {

		up=0;
		down=0;
		right=0;
		left=0;
		yy = gameObject.transform.position.y;
		xx = gameObject.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

		if(up==1){
			yy = yy + (float)(Time.deltaTime);
			if (yy >= 1.0f){
				yy=1.0f;
				up=0;
			}
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,yy,gameObject.transform.position.z);
		}
		if(down==1){

			yy = yy - (float)(Time.deltaTime);
			if (yy <= 0.0f){
				yy=0.01f;
				down=0;
			}
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,yy,gameObject.transform.position.z);
		}

		if(left==1){
			xx = xx - (float)(Time.deltaTime);
			if (xx <= 0.0f){
				xx=0.0f;
				left=0;
			}
			gameObject.transform.position = new Vector3(xx,gameObject.transform.position.y,gameObject.transform.position.z);
		}
		if(right==1){
			
			xx = xx + (float)(Time.deltaTime);
			if (xx >= 1.9f){
				xx=1.9f;
				right=0;
			}
			gameObject.transform.position = new Vector3(xx,gameObject.transform.position.y,gameObject.transform.position.z);
		}
	}
	public void UpdateUp(){

		if (up==1) {up=0;
		}
		else{
			up = 1;
		}
		down=0;
		right=0;
		left=0;
		yy = gameObject.transform.position.y;
	}
	public void UpdateDown(){
		up =0;
		right=0;
		left=0;
		if (down==1) {down=0;
		}
		else{
		down = 1;
		}
		yy = gameObject.transform.position.y;
	}
	public void UpdateLeft(){
		up =0;
		down = 0;
		right=0;
	
		if (left==1) {left=0;
		}
		else{
			left = 1;
		}
		xx = gameObject.transform.position.x;
	}
	public void UpdateRight(){
		up =0;
		down = 0;
	
		if (right==1) {right=0;
		}
		else{
			right = 1;
		}
		left=0;
		xx = gameObject.transform.position.x;
	}
}
