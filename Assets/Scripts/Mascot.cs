using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Mascot : MonoBehaviour
{
	public float FoodValue;
	public float MaxFoodValue = 10;
	public float MinFoodValue = -10;
	public const int ThunderValue = -7;
	public const int RainValue = -1;
	public float MinHappinessLength = 0.05f;
	public float MaxHappinessLength = 0.65f;
	public float RainAmbientIntensity = 0.5f;
	public float SunAmbientIntensity = 1f;
	public float ThunderAmbientIntensity = 0.3f;
	public GameObject HappinessMeter;
	public GameObject Leaf;
	public GameObject Thunder;
	public GameObject Rain;

	private int OrangeFoodEaten;
	private Vector3 initialScale;
	private Vector3 initialPosition;
	private AudioSource audioSource; // Reference to the audio source component
	private Renderer happinessRenderer;
	private Material happinessMaterial;
	private Material skyboxMaterial;
	private Renderer renderer;
	private Material material;
    private Renderer leafRenderer;
    private Material leafMaterial;
    private Color color;
	private Animator animator;

	void Start()
	{
		// Get the Skybox material
		skyboxMaterial = RenderSettings.skybox;
		// Make a new instance of the material so that we don't modify the global skybox material
		skyboxMaterial = new Material(skyboxMaterial);
		RenderSettings.skybox = skyboxMaterial;
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
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Food>() == null) return;
		var currentScaleY = HappinessMeter.transform.localScale.y;

		Food food = collision.gameObject.GetComponent<Food>();
		if (food.FoodClass == FoodClass.Orange)
		{
			OrangeFoodEaten++;
			if (OrangeFoodEaten >= 3)
			{
				food.FoodValue = -1;
			}
		}
		animator.SetTrigger(food.FoodValue > 0 ? "Jump" : "Sad");
		FoodValue = Mathf.Clamp(FoodValue + food.FoodValue, MinFoodValue, MaxFoodValue);
		audioSource.PlayOneShot(food.EatingSound);
		Destroy(food.gameObject);

		Weather();

		// Scale the happiness meter based on the current food value
		float scalePercentage = Mathf.InverseLerp(MinFoodValue, MaxFoodValue, FoodValue);
		float newScaleY = Mathf.Lerp(MinHappinessLength, MaxHappinessLength, scalePercentage);

		var positionDifference =  newScaleY- currentScaleY;
		StartCoroutine(ChangeHappinessMeterScale(newScaleY, HappinessMeter.transform.localPosition.y + positionDifference, 0.5f));
	}

	void Weather()
	{
		switch (FoodValue)
		{
			case <= ThunderValue:
				EnableWeatherEvent(Rain);
				EnableWeatherEvent(Thunder);
				if (Math.Abs(RenderSettings.ambientIntensity - ThunderAmbientIntensity) > 0.1f)
				{
					StartCoroutine(ChangeAmbientIntensity(ThunderAmbientIntensity, 5f));
				}
				break;
			case <= RainValue:
				DisableWeatherEvent(Thunder);
				EnableWeatherEvent(Rain);
				if (Math.Abs(RenderSettings.ambientIntensity - RainAmbientIntensity) > 0.1f)
				{
					StartCoroutine(ChangeAmbientIntensity(RainAmbientIntensity, 5f));
				}
				break;
			default:
				DisableWeatherEvent(Rain);
				DisableWeatherEvent(Thunder);
				if (Math.Abs(RenderSettings.ambientIntensity - SunAmbientIntensity) > 0.1f)
				{
					StartCoroutine(ChangeAmbientIntensity(SunAmbientIntensity, 5f));
				}
				break;
		}
	}

	void EnableWeatherEvent(GameObject gameObject)
	{
		if (gameObject.activeSelf) return;
		gameObject.SetActive(true);
		var sound = gameObject.GetComponent<AudioSource>();
		sound.volume = 0f;
		StartCoroutine(FadeIn(sound, 5f));
	}
	void DisableWeatherEvent(GameObject gameObject)
	{
		if (!gameObject.activeSelf) return;
		var sound = gameObject.GetComponent<AudioSource>();
		StartCoroutine(FadeOut(sound, 5f, gameObject));
	}
	public IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
	{
		float currentTime = 0;
		float startVolume = audioSource.volume;

		while (currentTime < fadeTime)
		{
			currentTime += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(startVolume, 1f, currentTime / fadeTime);
			yield return null;
		}
	}
	public IEnumerator ChangeAmbientIntensity(float intensity, float time)
	{
		float currentTime = 0;
		float startIntensity = RenderSettings.ambientIntensity;

		while (currentTime < time)
		{
			currentTime += Time.deltaTime;
			var newIntensity = Mathf.Lerp(startIntensity, intensity, currentTime / time);
			RenderSettings.ambientIntensity = newIntensity;
			skyboxMaterial.SetFloat("_Exposure", newIntensity);
			yield return null;
		}
	}

	public IEnumerator FadeOut(AudioSource audioSource, float fadeTime, GameObject gameObject)
	{
		float currentTime = 0;
		float startVolume = audioSource.volume;

		while (currentTime < fadeTime)
		{
			currentTime += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeTime);
			yield return null;
		}
		
		gameObject.SetActive(false);
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