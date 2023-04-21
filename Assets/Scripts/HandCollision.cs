using UnityEngine;

public class HandCollision : MonoBehaviour
{
	private SportPoints sportPoints;
	private string name;
	void Start()
	{
		name = gameObject.name;
		sportPoints = FindFirstObjectByType<SportPoints>();
	}
	void OnTriggerStay(Collider other)
	{
		if (other.transform.parent.name == "LeftHand (Teleport Locomotion)" & name == "Left")
			sportPoints.points++;
		if (other.transform.parent.name == "RightHand (Teleport Locomotion)" & name == "Right")
			sportPoints.points++;
	}
}