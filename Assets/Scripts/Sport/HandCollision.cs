using UnityEngine;

public class HandCollision : SportGame
{
	private string name;
	void Start()
	{
		name = gameObject.name;
	}
	void OnTriggerStay(Collider other)
	{
		if (other.transform.parent.name == "LeftHand (Teleport Locomotion)" & name == "Left")
			CurrentPoints++;
		if (other.transform.parent.name == "RightHand (Teleport Locomotion)" & name == "Right")
			CurrentPoints++;
	}
}