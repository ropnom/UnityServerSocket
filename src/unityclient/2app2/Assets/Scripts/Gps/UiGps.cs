using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiGps : MonoBehaviour
{
    /// <summary>
    /// The reference to the latitude value label.
    /// </summary>
    [SerializeField]
    private Text latitude;

    /// <summary>
    /// The reference to the longitude value label.
    /// </summary>
    [SerializeField]
    private Text longitude;

    /// <summary>
    /// Writes the latitude value in the corresponding
    /// UI text label.
    /// </summary>
    /// <param name="value">The latitude value.</param>
    public void WriteLatitudeValue(string value)
    {
        latitude.text = value;
    }


    /// <summary>
    /// Writes the longitude in the corresponding
    /// UI text label.
    /// </summary>
    /// <param name="value">The longitude value.</param>
    public void WriteLongitudeValue(string value)
    {
        longitude.text = value;
    }
}
