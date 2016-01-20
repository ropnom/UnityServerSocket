using UnityEngine;
using System.Collections;

public class Gps : MonoBehaviour
{
    /// <summary>
    /// The reference to the UI manager.
    /// </summary>
    private UiGps uiGps;

    void Awake()
    {
        uiGps = GameObject.FindGameObjectWithTag("Ui").GetComponent<UiGps>();
    }

    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
    }

    void Update()
    {
        uiGps.WriteLatitudeValue(Input.location.lastData.latitude.ToString());
        uiGps.WriteLongitudeValue(Input.location.lastData.longitude.ToString());
    }
}
