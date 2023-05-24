using System;
using UnityEngine;

public class Food : MonoBehaviour
{
	public FoodClass FoodClass;
	[NonSerialized]
	public float FoodValue = 0;
	public AudioClip EatingSound;
	private Vector3 position;
	private Quaternion rotation;

	void Awake()
	{
		position = transform.position;
		rotation = transform.rotation;
		// Set the food value based on the food class
		switch (FoodClass)
		{
			case FoodClass.DarkGreen:
				FoodValue = 2;
				break;
			case FoodClass.Green:
				FoodValue = 1f;
				break;
			case FoodClass.Orange:
				FoodValue = 0.5f;
				break;
			case FoodClass.Red:
				FoodValue = -2;
				break;
		}
	}

	void Update()
	{
		if (!(transform.position.y < -5)) return;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.rotation = rotation;
		transform.position = position;
	}
}