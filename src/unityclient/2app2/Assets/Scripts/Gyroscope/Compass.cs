using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Compass : MonoBehaviour
{
    /// <summary>
    /// The degrees to the North.
    /// </summary>
    public float Heading { get; set; }

    /// <summary>
    /// Reference to the UI manager object.
    /// </summary>
    private UiManager uiManager;

    // Called when script is loaded.
    void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("Ui").GetComponent<UiManager>();
    }

	// Use this for initialization
	void Start () 
    {
        Input.compass.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        Heading = Input.compass.trueHeading;
        uiManager.WriteCompassValue(Heading.ToString());
	}
}
