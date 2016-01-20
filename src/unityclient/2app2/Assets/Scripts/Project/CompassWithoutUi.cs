using UnityEngine;
using System.Collections;

public class CompassWithoutUi : MonoBehaviour {

    /// <summary>
    /// The degrees to the North.
    /// </summary>
    public float Heading { get; set; }

    // Use this for initialization
    void Start()
    {
        Input.compass.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Heading = Input.compass.trueHeading;
    }
}
