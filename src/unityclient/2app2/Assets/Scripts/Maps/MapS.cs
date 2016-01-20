using UnityEngine;
using System.Collections;

public class MapS : MonoBehaviour {

	[SerializeField]
	private int X;
	[SerializeField]
	private int Y;

	private Texture2D zoom14;
	private Texture2D zoom15;
	private Texture2D zoom16;
	private Texture2D zoom17;
	private Texture2D zoom18;

	int zoom =15;


	// Use this for initialization

	IEnumerator Start () {
		zoom14 = new Texture2D(1, 1, TextureFormat.ARGB32, true);
		zoom15 = new Texture2D(1, 1, TextureFormat.ARGB32, true);
		zoom16 = new Texture2D(1, 1, TextureFormat.ARGB32, true);
		zoom17 = new Texture2D(1, 1, TextureFormat.ARGB32, true);
		zoom18 = new Texture2D(1, 1, TextureFormat.ARGB32, true);

		double lon = float.Parse(PositionUtilities.centerMapLon);
		double lat = float.Parse(PositionUtilities.centerMapLat);

		Point p = PositionUtilities.WorldToTilePosZoom(lon, lat, 14);
		int x = Mathf.FloorToInt((float)p.X);
		int y = Mathf.FloorToInt((float)p.Y);
		x= x+ X;
		y= y+ Y;
		WWW www = new WWW("http://a.tile.openstreetmap.org/14/"+ x + "/ " + y + ".png");
		yield return www;
		www.LoadImageIntoTexture(zoom14);

	    p = PositionUtilities.WorldToTilePosZoom(lon, lat, 15);
		x = Mathf.FloorToInt((float)p.X);
		y = Mathf.FloorToInt((float)p.Y);
		x= x+ X;
		y= y+ Y;
		www = new WWW("http://a.tile.openstreetmap.org/15/"+ x + "/ " + y + ".png");
		yield return www;
		www.LoadImageIntoTexture(zoom15);

		p = PositionUtilities.WorldToTilePosZoom(lon, lat, 16);
		x = Mathf.FloorToInt((float)p.X);
		y = Mathf.FloorToInt((float)p.Y);
		x= x+ X;
		y= y+ Y;
		www = new WWW("http://a.tile.openstreetmap.org/16/"+ x + "/ " + y + ".png");
		yield return www;
		www.LoadImageIntoTexture(zoom16);

		p = PositionUtilities.WorldToTilePosZoom(lon, lat, 17);
		x = Mathf.FloorToInt((float)p.X);
		y = Mathf.FloorToInt((float)p.Y);
		x= x+ X;
		y= y+ Y;
		www = new WWW("http://a.tile.openstreetmap.org/17/"+ x + "/ " + y + ".png");
		yield return www;
		www.LoadImageIntoTexture(zoom17);


		p = PositionUtilities.WorldToTilePosZoom(lon, lat, 18);
		x = Mathf.FloorToInt((float)p.X);
		y = Mathf.FloorToInt((float)p.Y);
		x= x+ X;
		y= y+ Y;
		www = new WWW("http://a.tile.openstreetmap.org/18/"+ x + "/ " + y + ".png");
		yield return www;
		www.LoadImageIntoTexture(zoom18);

		//gameObject.GetComponent<Renderer>().material.mainTexture = zoom14;
		zoom=0;
	}
	
	// Update is called once per frame
	void Update () {

		if (zoom!=PositionUtilities.Zoom){
			zoom=PositionUtilities.Zoom;
			//Debug.Log ("Canvio de Zoom" + PositionUtilities.Zoom);
			switch(PositionUtilities.Zoom){
			case 14:
				gameObject.GetComponent<Renderer>().material.mainTexture = zoom14;
				zoom = 14;
				break;
			case 15:
				gameObject.GetComponent<Renderer>().material.mainTexture = zoom15;
				zoom = 15;
				break;
			case 16:
				gameObject.GetComponent<Renderer>().material.mainTexture = zoom16;
				zoom= 16;
				break;
			case 17:
				gameObject.GetComponent<Renderer>().material.mainTexture = zoom17;
				zoom= 17;
				break;
			case 18:
				gameObject.GetComponent<Renderer>().material.mainTexture = zoom18;
				zoom=18;
				break; 
			}
		}
	}
}
