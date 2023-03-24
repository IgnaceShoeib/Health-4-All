using UnityEngine;

public class Mascot : MonoBehaviour
{
	public int FoodValue = 0;
	public int MaxFoodValue = 10;
	public int MinFoodValue = -10;
	public float MinHappinessLength = 0.05f;
	public float MaxHappinessLength = 0.65f;
	public GameObject HappinessMeter;

	private Vector3 initialScale;
	private AudioSource audioSource; // Reference to the audio source component

	void Start()
	{
		// Store the initial scale and position of the happiness meter
		initialScale = HappinessMeter.transform.localScale;

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

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Food>() != null)
		{
			var currentPositionY = HappinessMeter.transform.localPosition.y;
			var currentScaleY = HappinessMeter.transform.localScale.y;

			Food food = collision.gameObject.GetComponent<Food>();
			FoodValue = Mathf.Clamp(FoodValue + food.FoodValue, MinFoodValue, MaxFoodValue);
			audioSource.PlayOneShot(food.EatingSound);
			Destroy(food.gameObject);

			// Scale the happiness meter based on the current food value
			float scalePercentage = Mathf.InverseLerp(MinFoodValue, MaxFoodValue, FoodValue);
			float newScaleY = Mathf.Lerp(MinHappinessLength, MaxHappinessLength, scalePercentage);
			HappinessMeter.transform.localScale = new Vector3(initialScale.x, newScaleY, initialScale.z);

			// Update the emission color based on the happiness meter's scale
			Renderer renderer = HappinessMeter.GetComponent<Renderer>();
			Material material = renderer.material;

			float colorPercentage = Mathf.InverseLerp(MinHappinessLength, MaxHappinessLength, newScaleY);
			Color emissionColor = Color.Lerp(Color.red, Color.green, colorPercentage);
			material.SetColor("_EmissionColor", emissionColor);

			var positionDifference =  newScaleY- currentScaleY;
			HappinessMeter.transform.localPosition = new Vector3(HappinessMeter.transform.localPosition.x, HappinessMeter.transform.localPosition.y + positionDifference, HappinessMeter.transform.localPosition.z);
		}
	}
}