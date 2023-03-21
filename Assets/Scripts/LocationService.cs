using UnityEngine;
using System.Runtime.InteropServices;

public class LocationService : MonoBehaviour
{
	public static GeoPoint GeoPoint;
	public static double? Heading;
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void RequestLocation();
    [DllImport("__Internal")]
    private static extern void RequestHeading();
#endif

	// Start is called before the first frame update
	void Start()
	{
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
		SendHeadingRequest();
		SendLocationRequest();
#endif
	}

	public void SendLocationRequest()
	{
#if UNITY_WEBGL && !UNITY_EDITOR
        RequestLocation();
#endif
	}
	public void SendHeadingRequest()
	{
#if UNITY_WEBGL && !UNITY_EDITOR
        RequestHeading();
#endif
	}

	public void GetLocation(string location)
	{
		GeoPoint.Latitude = double.Parse(location.Split(',')[0]);
		GeoPoint.Longitude = double.Parse(location.Split(',')[1]);
		print("lat: " + GeoPoint.Latitude + " long: " + GeoPoint.Longitude);
		FindObjectOfType<GeoObjectSpawner>().SpawnGeoObjects(GeoPoint,Heading);
	}
	public void GetHeading(double heading)
	{
		Heading = heading;
		print(heading);
		FindObjectOfType<GeoObjectSpawner>().SpawnGeoObjects(GeoPoint, Heading);
	}
}