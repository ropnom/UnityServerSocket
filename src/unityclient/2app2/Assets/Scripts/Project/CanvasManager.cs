using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : MonoBehaviour 
{
    [SerializeField]
    private Text value1;

    [SerializeField]
    private Text value2;

    [SerializeField]
    private Text value3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetValueField1(string text)
    {
        value1.text = text;
    }
    public void SetValueField2(string text)
    {
        value2.text = text;
    }
    public void SetValueField3(string text)
    {
        value3.text = text;
    }
}
