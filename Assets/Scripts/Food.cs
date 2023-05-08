using System;
using UnityEngine;

public class Food : MonoBehaviour
{
	public FoodClass FoodClass;
	[NonSerialized]
	public float FoodValue = 0;
	public AudioClip EatingSound;

	void Start()
	{
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
}