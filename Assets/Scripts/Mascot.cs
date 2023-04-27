using System.Collections;
using UnityEngine;

public class Mascot : MonoBehaviour
{
	public int FoodValue = 0;
	public int MaxFoodValue = 10;
	public int MinFoodValue = -10;
	public const int ThunderValue = -7;
	public const int RainValue = -1;
	public const int CloudyValue = 0;
	public const int SunValue = 5;
	public float MinHappinessLength = 0.05f;
	public float MaxHappinessLength = 0.65f;
	public GameObject HappinessMeter;
	public GameObject Leaf;

	private Weather_Controller weatherController;
	private Weather_Controller.WeatherType desiredWeather;
	private Vector3 initialScale;
	private Vector3 initialPosition;
	private AudioSource audioSource; // Reference to the audio source component
	private Renderer happinessRenderer;
	private Material happinessMaterial;
    private Renderer renderer;
	private Material material;
    private Renderer leafRenderer;
    private Material leafMaterial;
    private Color color;
	private Animator animator;

	void Start()
	{
		//get weather controller
		weatherController = FindAnyObjectByType<Weather_Controller>();
		//get current weather
		desiredWeather = weatherController.en_CurrWeather;
		//get animator
		animator = gameObject.GetComponentInParent<Animator>();
		// get happiness meter material
		happinessRenderer = HappinessMeter.GetComponent<Renderer>();
		happinessMaterial = happinessRenderer.material;
        // get material
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        leafRenderer = Leaf.GetComponent<Renderer>();
        leafMaterial = leafRenderer.material;
        color = material.color;
        // Store the initial scale and position of the happiness meter
        initialScale = HappinessMeter.transform.localScale;
		initialPosition = HappinessMeter.transform.localPosition;

		// Try to get an existing AudioSource component on the same object
		audioSource = GetComponent<AudioSource>();

		// If no AudioSource component exists, add one to the same object
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
		// Make sound 3D
		audioSource.spatialBlend = 1;
		audioSource.maxDistance = 10;
	}
	public void Update()
	{
		switch (FoodValue)
		{
			case <= ThunderValue:
				desiredWeather = Weather_Controller.WeatherType.THUNDERSTORM;
				break;
			case <= RainValue:
				desiredWeather = Weather_Controller.WeatherType.RAIN;
				break;
			case <= CloudyValue:
				desiredWeather = Weather_Controller.WeatherType.CLOUDY;
				break;
			case >= SunValue:
				desiredWeather = Weather_Controller.WeatherType.SUN;
				break;
		}
		if (desiredWeather == weatherController.en_CurrWeather)
			return;
		weatherController.ExitCurrentWeather((int)desiredWeather);
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Food>() != null)
		{
			var currentScaleY = HappinessMeter.transform.localScale.y;

			Food food = collision.gameObject.GetComponent<Food>();
			animator.SetTrigger(food.FoodValue > 0 ? "Jump" : "Sad");
			FoodValue = Mathf.Clamp(FoodValue + food.FoodValue, MinFoodValue, MaxFoodValue);
			audioSource.PlayOneShot(food.EatingSound);
			Destroy(food.gameObject);

			// Scale the happiness meter based on the current food value
			float scalePercentage = Mathf.InverseLerp(MinFoodValue, MaxFoodValue, FoodValue);
			float newScaleY = Mathf.Lerp(MinHappinessLength, MaxHappinessLength, scalePercentage);

			var positionDifference =  newScaleY- currentScaleY;
			StartCoroutine(ChangeHappinessMeterScale(newScaleY, HappinessMeter.transform.localPosition.y + positionDifference, 0.5f));
		}
	}

	IEnumerator ChangeHappinessMeterScale(float newScaleY, float newPositionY, float duration)
	{
		float initialScaleY = HappinessMeter.transform.localScale.y;
		float initialPositionY = HappinessMeter.transform.localPosition.y;
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / duration);
			float currentScaleY = Mathf.Lerp(initialScaleY, newScaleY, t);
			float currentPositionY = Mathf.Lerp(initialPositionY, newPositionY, t);
			HappinessMeter.transform.localScale = new Vector3(initialScale.x, currentScaleY, initialScale.z);
			HappinessMeter.transform.localPosition = new Vector3(initialPosition.x, currentPositionY, initialPosition.z);
			// Update the emission color based on the happiness meter's scale
			float colorPercentage = Mathf.InverseLerp(MinHappinessLength, MaxHappinessLength, currentScaleY);
			Color emissionColor = Color.Lerp(Color.red, Color.green, colorPercentage);
			happinessMaterial.SetColor("_EmissionColor", emissionColor);
			// Update the mascot's material
            if (colorPercentage <= 0.5f)
            {
                Color baseColor = Color.Lerp(Color.grey, color, colorPercentage*2);
                material.color = baseColor;
                leafMaterial.color = baseColor;
            }
            else
            {
                Color baseColor = Color.Lerp(color, new Color(0.2917078f,1,0), (colorPercentage-0.5f)*2);
                material.color= baseColor;
                leafMaterial.color= baseColor;
            }
            yield return null;
		}
	}
}