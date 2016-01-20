using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.LookAt(mainCamera.transform);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y + 180F, gameObject.transform.rotation.eulerAngles.z));
	}
}
