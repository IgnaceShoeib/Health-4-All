using UnityEngine;
using System.Runtime.InteropServices;

public class LocationService : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void RequestLocation();
    [DllImport("__Internal")]
    private static extern void RequestWatch();
#endif

	// Start is called before the first frame update
	void Start()
	{
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
		SendWatchRequest();
		SendLocationRequest();
#endif
	}

	public void SendLocationRequest()
	{
#if UNITY_WEBGL && !UNITY_EDITOR
        RequestLocation();
#endif
	}
	public void SendWatchRequest()
	{
#if UNITY_WEBGL && !UNITY_EDITOR
        RequestWatch();
#endif
	}

	public void GetLocation(string location)
	{
		double latitude = double.Parse(location.Split(',')[0]);
		double longitude = double.Parse(location.Split(',')[1]);
		print("lat: " + latitude + " long: " + longitude);
		FindObjectOfType<GeoConverter>().SpawnGeoObjects(new GeoPoint(latitude, longitude));
	}
	public void GetWatch(double heading)
	{
		print(heading);
	}
}