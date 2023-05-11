using System;
using UnityEngine;

[Serializable]
public class FoodCombo
{
	public Food[] Food = new Food[2];
	public Vector3[] BubblePosition = new Vector3[2];
	public Vector3[] BubbleRotation = new Vector3[2];
	public Vector3[] BubbleScale = new Vector3[2];
}