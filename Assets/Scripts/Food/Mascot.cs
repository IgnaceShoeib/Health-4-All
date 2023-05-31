using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Mascot : MonoBehaviour
{
	public float FoodValue;
	public float MaxFoodValue = 10;
	public float MinFoodValue = -10;
	public const int ThunderValue = -7;
	public const int RainValue = -1;
	public float RainAmbientIntensity = 0.5f;
	public float SunAmbientIntensity = 1f;
	public float ThunderAmbientIntensity = 0.3f;
	public GameObject Leaf;
	public GameObject Thunder;
	public GameObject Rain;
	public GameObject Bubble;
	public GameObject Score;
	public Renderer CrossRenderer;
	public Renderer CheckRenderer;
	public List<FoodCombo> FoodCombos;

	private int AccumulatedFoodValue;
	private int OrangeFoodEaten;
	private float fadeDuration = 0.5f;
	private AudioSource audioSource; // Reference to the audio source component
	private Material skyboxMaterial;
	private Material checkMaterial;
	private Material crossMaterial;
	private Renderer renderer;
	private Material material;
    private Renderer leafRenderer;
    private Material leafMaterial;
    private Color color;
	private Color scoreColor;
	private Animator animator;
	private FoodCombo foodCombo;

	void Start()
	{
		// Get the Skybox material
		skyboxMaterial = RenderSettings.skybox;
		// Make a new instance of the material so that we don't modify the global skybox material
		skyboxMaterial = new Material(skyboxMaterial);
		RenderSettings.skybox = skyboxMaterial;
		//get animator
		animator = gameObject.GetComponentInParent<Animator>();
        // get material
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        leafRenderer = Leaf.GetComponent<Renderer>();
        leafMaterial = leafRenderer.material;
		checkMaterial = CheckRenderer.material;
		crossMaterial = CrossRenderer.material;
        color = material.color;

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

		// select the initial foodcombo
		SelectFoodCombo();
	}

	private void SelectFoodCombo()
	{
		if (FoodCombos.Count == 0 | AccumulatedFoodValue >= MaxFoodValue)
		{
			Bubble.SetActive(false);
			float score = FoodValue;
			score = (score - MinFoodValue) / (MaxFoodValue - MinFoodValue) * 100;
			Score.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(score).ToString();
			Score.GetComponent<TextMeshProUGUI>().color = scoreColor;
			Score.transform.parent.gameObject.SetActive(true);
			return;
		}
		var randomIndex = UnityEngine.Random.Range(0, FoodCombos.Count);
		foodCombo = FoodCombos[randomIndex];

		if (foodCombo.Food[0].FoodValue >= foodCombo.Food[1].FoodValue)
			AccumulatedFoodValue += (int)foodCombo.Food[0].FoodValue;
		else
			AccumulatedFoodValue += (int)foodCombo.Food[1].FoodValue;

		MakeBubbleFood(0);
		MakeBubbleFood(1);

		FoodCombos.RemoveAt(randomIndex);
		var outline1 = foodCombo.Food[0].gameObject.AddComponent<Outline>();
		var outline2 = foodCombo.Food[1].gameObject.AddComponent<Outline>();
		outline1.OutlineMode = Outline.Mode.OutlineAll;
		outline2.OutlineMode = Outline.Mode.OutlineAll;
		outline1.OutlineColor = Color.red;
		outline2.OutlineColor = Color.red;
		outline1.OutlineWidth = 10f;
		outline2.OutlineWidth = 10f;
	}

	private void MakeBubbleFood(int food)
	{
		if (Bubble.transform.childCount == 2)
		{
			for (int i = 0; i < Bubble.transform.childCount; i++)
			{
				Destroy(Bubble.transform.GetChild(i).gameObject);
			}
		}
		var bubblefood = Instantiate(foodCombo.Food[food].gameObject);
		Destroy(bubblefood.GetComponent<CollisionSound>());
		Destroy(bubblefood.GetComponent<Food>());
		Destroy(bubblefood.GetComponent<Collider>());
		Destroy(bubblefood.GetComponent<XRGrabInteractable>());
		Destroy(bubblefood.GetComponent<Rigidbody>());
		bubblefood.transform.parent = Bubble.transform;
		bubblefood.transform.localPosition = foodCombo.BubblePosition[food];
		bubblefood.transform.localScale = foodCombo.BubbleScale[food];
		bubblefood.transform.localEulerAngles = foodCombo.BubbleRotation[food];
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Food>() == null || collision.gameObject.GetComponent<Outline>()==null) return;

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
		StartCoroutine(food.FoodValue > 0 ? FadeMaterial(checkMaterial) : FadeMaterial(crossMaterial));

		FoodValue = Mathf.Clamp(FoodValue + food.FoodValue, MinFoodValue, MaxFoodValue);
		audioSource.PlayOneShot(food.EatingSound);

		Destroy(foodCombo.Food[0].gameObject.GetComponent<Outline>());
		Destroy(foodCombo.Food[1].gameObject.GetComponent<Outline>());
		Destroy(food.gameObject);
		SelectFoodCombo();

		Weather();

		StartCoroutine(CalculateColor(0.5f));
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
	IEnumerator CalculateColor(float duration)
	{
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			// Update the emission color based on the happiness meter's scale
			float colorPercentage = Mathf.InverseLerp(MinFoodValue, MaxFoodValue, FoodValue);
			scoreColor = Color.Lerp(Color.red, Color.green, colorPercentage);
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
	public IEnumerator FadeMaterial(Material material)
	{
		// Fade in the material
		yield return StartCoroutine(FadeMaterialIn(material));

		// Keep the material visible for the specified stay duration
		yield return new WaitForSeconds(1);

		// Fade out the material
		yield return StartCoroutine(FadeMaterialOut(material));
	}
	private IEnumerator FadeMaterialIn(Material material)
	{
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			// Calculate the current alpha value based on the elapsed time
			float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

			// Set the material's color with the updated alpha value
			material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	private IEnumerator FadeMaterialOut(Material material)
	{
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			// Calculate the current alpha value based on the elapsed time
			float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

			// Set the material's color with the updated alpha value
			material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);

			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}