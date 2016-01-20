using UnityEngine;
using System.Collections;

public struct Point
{
    public double X;
    public double Y;
}

public static class PositionUtilities
{

    public static int TileX;
    public static int TileY;
    public static int Zoom = 14;
    public static Vector3 PlaneLocalScale = new Vector3(1, 1, 1);
	//public static string centerMapLat="41.275264";
	//public static string centerMapLon="1.98729";
	public static string centerMapLat="41.389310";
	public static string centerMapLon="2.194775";
	//public static double Longitude = 1.98729;
	//public static double Latitude = 41.275264;
	public static double Longitude = 2.194775;
	public static double Latitude = 41.389310;


    public static void Init()
    {
		//centerMapLat = "41.275264";
		//centerMapLon = "1.98729";
		//centerMapLat = "41.389310";
		//centerMapLon = "2.194775";
        double lon = float.Parse(centerMapLon);
        double lat = float.Parse(centerMapLat);
        Point p = PositionUtilities.WorldToTilePos(lon, lat);
        TileX = Mathf.FloorToInt((float)p.X);
        TileY = Mathf.FloorToInt((float)p.Y);
    }

    public static Point WorldToTilePos(double lon, double lat)
    {
        Point p = new Point();
        p.X = ((lon + 180.0) / 360.0 * System.Math.Pow(2.0, Zoom));
        p.Y = ((1.0 - System.Math.Log(System.Math.Tan(lat * System.Math.PI / 180.0) +
            1.0 / System.Math.Cos(lat * System.Math.PI / 180.0)) / System.Math.PI) / 2.0 * System.Math.Pow(2.0, Zoom));

        return p;
    }
	public static Point WorldToTilePosZoom(double lon, double lat, int Z)
	{
		Point p = new Point();
		p.X = ((lon + 180.0) / 360.0 * System.Math.Pow(2.0, Z));
		p.Y = ((1.0 - System.Math.Log(System.Math.Tan(lat * System.Math.PI / 180.0) +
		                              1.0 / System.Math.Cos(lat * System.Math.PI / 180.0)) / System.Math.PI) / 2.0 * System.Math.Pow(2.0, Z));
		
		return p;
	}
    public static Point TileToWorldPos(double tile_x, double tile_y, int zoom)
    {
        Point p = new Point();
        double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, zoom));

        p.X = ((tile_x / System.Math.Pow(2.0, zoom) * 360.0) - 180.0);
        p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));

        return p;
    }

    public static double DrawCubeY(double targetLat)
    {
        return GetCubeY(targetLat, PositionUtilities.TileToWorldPos(PositionUtilities.TileX, PositionUtilities.TileY + 1, PositionUtilities.Zoom).Y, PositionUtilities.TileToWorldPos(PositionUtilities.TileX, PositionUtilities.TileY, PositionUtilities.Zoom).Y);
    }

    private static double GetCubeY(double targetLat, double minLat, double maxLat)
    {
        double pixelY = ((targetLat - minLat) / (maxLat - minLat)) * PlaneLocalScale.x;
        return pixelY;
    }

    public static double DrawCubeX(double targetLong)
    {
        return GetCubeX(targetLong, PositionUtilities.TileToWorldPos(PositionUtilities.TileX, PositionUtilities.TileY, PositionUtilities.Zoom).X, PositionUtilities.TileToWorldPos(PositionUtilities.TileX + 1, PositionUtilities.TileY, PositionUtilities.Zoom).X);
    }

    private static double GetCubeX(double targetLong, double minLong, double maxLong)
    {
        double pixelX = ((targetLong - minLong) / (maxLong - minLong)) * PlaneLocalScale.z;
        return pixelX;
    }
}