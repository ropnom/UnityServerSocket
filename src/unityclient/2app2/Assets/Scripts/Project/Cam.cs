using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cam : MonoBehaviour {

    [SerializeField]
    private MeshRenderer background;

    GpsWithoutUi gps;
    WebCamTexture texture;
    CanvasManager cm;

	public void CloseCamera()
	{
		texture.Stop ();
	}

    void Awake()
    {
        gps = GameObject.FindGameObjectWithTag("Gps").GetComponent<GpsWithoutUi>();
        cm = GameObject.FindGameObjectWithTag("Ui").GetComponent<CanvasManager>();
        PositionUtilities.Init();


    }

	// Use this for initialization
	void Start () {

	    texture = new WebCamTexture();
        texture.Play();
	
	}
	
	// Update is called once per frame
	void Update () {
        background.material.mainTexture = texture;
        string lat = gps.Latitude.ToString();
        string lng = gps.Longitude.ToString();

        cm.SetValueField3(lat + "  " + lng);

        if (lat != "0")
        {
            float z = (float)PositionUtilities.DrawCubeY((double)gps.Latitude);
            float x = (float)PositionUtilities.DrawCubeX((double)gps.Longitude);
		
		

            gameObject.transform.position = new Vector3(x, 0, z);

        }

        cm.SetValueField1(transform.position.x + "  " + 
                            transform.position.y + "  " + 
                            transform.position.z);
        cm.SetValueField2(transform.rotation.eulerAngles.x + "  " + 
                            transform.rotation.eulerAngles.y + "  " + 
                            transform.rotation.eulerAngles.z);
        
	}
}
