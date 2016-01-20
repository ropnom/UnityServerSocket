using UnityEngine;
using System.Collections;

public class loadScene : MonoBehaviour {

	private GameObject mainCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void mapScene(){
		Screen.orientation = ScreenOrientation.Portrait;
		Application.LoadLevel("MapOnly");
	}
	public void cameraScene(){
		Screen.orientation = ScreenOrientation.Portrait;
		Application.LoadLevel("App_Session1");
	}
	public void calibrationScene(){
		Screen.orientation = ScreenOrientation.Portrait;
		Application.LoadLevel("Calibration");
	}
	public void menuScenefromCamera(){
		Screen.orientation = ScreenOrientation.Portrait;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		mainCamera.GetComponent<Cam>().CloseCamera();
		Application.LoadLevel("Menu");
	}
	public void menuScene()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		Application.LoadLevel("Menu");
	}
}
