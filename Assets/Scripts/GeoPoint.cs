using System;

[Serializable]
public class GeoPoint
{
	public double Latitude;
	public double Longitude;

	public GeoPoint(double latitude, double longitude)
	{
		Latitude = latitude;
		Longitude = longitude;
	}
}