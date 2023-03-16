using UnityEngine;

public class Mascot : MonoBehaviour
{
	public int FoodValue = 0;
	void OnCollisionEnter(Collision collision)
	{
		print("bruh");
		if (collision.gameObject.GetComponent<Food>() != null)
		{
			Food food = collision.gameObject.GetComponent<Food>();
			FoodValue += food.FoodValue;
			Destroy(food.gameObject);
		}
	}
}