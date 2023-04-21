using System.Collections;
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
	private Vector3 initialPosition;
	private AudioSource audioSource; // Reference to the audio source component
	private Renderer renderer;
	private Material material;
	private Animator animator;

	void Start()
	{
		//get animator
		animator = gameObject.GetComponentInParent<Animator>();
		// get material
		renderer = HappinessMeter.GetComponent<Renderer>();
		material = renderer.material;
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
			material.SetColor("_EmissionColor", emissionColor);
			yield return null;
		}
	}
}