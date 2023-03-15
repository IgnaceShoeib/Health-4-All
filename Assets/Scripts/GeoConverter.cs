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
	public double Haversine(GeoPoint geoPoint1, GeoPoint geoPoint2)
    {
        double R = 6371; // Radius of the earth in km
        double dLat = (geoPoint2.Latitude - geoPoint1.Latitude) * (Math.PI / 180);
        double dLon = (geoPoint2.Longitude - geoPoint1.Longitude) * (Math.PI / 180);
        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(geoPoint1.Latitude * (Math.PI / 180)) * Math.Cos(geoPoint2.Latitude * (Math.PI / 180)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double d = R * c; // Distance in km
        return d * 1000; // Distance in m
    }

	// Convert geographic coordinates to in-game coordinates
	public Vector3 ConvertGeoToGame(GeoPoint geoPoint)
    {
        // Calculate the distance between the reference point and the geographic coordinates
        double distance = Haversine(UserGeoPoint, geoPoint);

        // Convert the distance to the appropriate units for your game world
        // In this example, we assume the game world uses meters as its unit of distance
        double distanceInGameUnits = distance;

        // Calculate the object's position in the game world relative to the reference point
        double objectX = distanceInGameUnits * Mathf.Cos((float)geoPoint.Longitude) * Mathf.Cos((float)geoPoint.Latitude);
        double objectY = 1;
        double objectZ = distanceInGameUnits * Mathf.Cos((float)geoPoint.Longitude) * Mathf.Sin((float)geoPoint.Latitude)*-1;

        // Return the object's position as a Vector3 object
        return new Vector3((float)objectX, (float)objectY, (float)objectZ) + referencePoint;
    }

	public void SpawnGeoObjects(GeoPoint user, double? heading)
	{
		if (heading == null || user.Latitude == 0 || user.Longitude == 0 || user == null || user.Latitude == null || user.Longitude == null)
		{
			return;
		}

		UserGeoPoint = user;
		// Convert the user's coordinates to in-game coordinates
		Vector3 userPosition = ConvertGeoToGame(UserGeoPoint);

		// Convert the object's coordinates to in-game coordinates
		Vector3 objectPosition = ConvertGeoToGame(ObjectGeoPoint);

		var spawnedObject = Instantiate(Object, objectPosition, new Quaternion());
        spawnedObject.transform.RotateAround(userPosition, Vector3.up, (float)heading*-1);
        Debug.Log("Object position: " + spawnedObject.transform.position);
	}
}
