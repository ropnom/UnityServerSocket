using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour
{
    /// <summary>
    /// The reference to the compass value label.
    /// </summary>
    [SerializeField]
    private Text compass;

    /// <summary>
    /// The reference to the gyroscope value label.
    /// </summary>
    [SerializeField]
    private Text gyro;

    /// <summary>
    /// Writes the compass Heading value in the corresponding
    /// UI text label.
    /// </summary>
    /// <param name="value">The compass true heading value.</param>
    public void WriteCompassValue(string value)
    {
        compass.text = value;
    }


    /// <summary>
    /// Writes the gyroscope value in the corresponding
    /// UI text label.
    /// </summary>
    /// <param name="value">The gyroscope attitude.</param>
    public void WriteGyroValue(string value)
    {
        gyro.text = value;
    }
}
