using System;
using UnityEngine;

public class GeoConverter : MonoBehaviour
{
	// Define a reference point in the game world
	public Vector3 referencePoint;

	// Define the user's coordinates and the object's coordinates
	private GeoPoint UserGeoPoint;
	public GeoPoint ObjectGeoPoint;

	public GameObject Object;

	// Calculate the distance between two points on a sphere using the Haversine formula
	public double Haversine(double lat1, double lon1, double lat2, double lon2)
	{
		double R = 6371; // Radius of the earth in km
		double dLat = (lat2 - lat1) * (Math.PI / 180);
		double dLon = (lon2 - lon1) * (Math.PI / 180);
		double a =
			Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
			Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) *
			Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
		double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
		double d = R * c; // Distance in km
		return d * 1000; // Distance in m
	}

	// Convert geographic coordinates to in-game coordinates
	public Vector3 ConvertGeoToGame(double latitude, double longitude)
	{
		// Calculate the distance between the reference point and the geographic coordinates
		double distance = Haversine(UserGeoPoint.Latitude, UserGeoPoint.Longitude, latitude, longitude);

		// Convert the distance to the appropriate units for your game world
		// In this example, we assume the game world uses meters as its unit of distance
		double distanceInGameUnits = distance;

		// Calculate the object's position in the game world
		double objectX = referencePoint.x +
		                 distanceInGameUnits * Mathf.Cos((float)longitude) * Mathf.Cos((float)latitude);
		double objectY = 1;
		double objectZ = referencePoint.z + distance * Mathf.Cos((float)longitude) * Mathf.Sin((float)latitude);

		// Return the object's position as a Vector3 object
		return new Vector3((float)objectX, (float)objectY, (float)objectZ);
	}

	public void SpawnGeoObjects(GeoPoint user)
	{
		UserGeoPoint = user;
		// Convert the user's coordinates to in-game coordinates
		Vector3 userPosition = ConvertGeoToGame(UserGeoPoint.Latitude, UserGeoPoint.Latitude);

		// Convert the object's coordinates to in-game coordinates
		Vector3 objectPosition = ConvertGeoToGame(ObjectGeoPoint.Latitude, ObjectGeoPoint.Longitude);

		// Use the user and object positions to do whatever you need to do in your game
		Debug.Log("User position: " + userPosition);
		Debug.Log("Object position: " + objectPosition);

		Instantiate(Object, objectPosition,new Quaternion());
	}
}
