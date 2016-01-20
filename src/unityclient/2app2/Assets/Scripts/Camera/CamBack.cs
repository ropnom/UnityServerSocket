using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CamBack : MonoBehaviour {

    private WebCamTexture texture;

    [SerializeField]
    private MeshRenderer background;

	// Use this for initialization
	void Start ()
    {
	    texture = new WebCamTexture();
        texture.Play();
	}
	
	// Update is called once per frame
	void Update () 
    {
        background.material.mainTexture = texture;
	}
}
