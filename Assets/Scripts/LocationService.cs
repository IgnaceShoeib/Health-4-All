using UnityEngine;
using System.Runtime.InteropServices;

public class LocationService : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void RequestLocation();
#endif

	// Start is called before the first frame update
	void Start()
	{
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
		SendLocationRequest();
#endif
	}

	public void SendLocationRequest()
	{
#if UNITY_WEBGL && !UNITY_EDITOR
        RequestLocation();
#endif
	}

	public void GetLocation(string location)
	{
		double latitude = double.Parse(location.Split(',')[0]);
		double longitude = double.Parse(location.Split(',')[1]);
		print("lat: " + latitude + " long: " + longitude);
	}

}