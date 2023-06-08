using UnityEngine;

public class HandCollision : MonoBehaviour
{
	private string name;
	public MovementController ActiveMovementController;
	void Start()
	{
		name = gameObject.name;
	}
	void OnTriggerStay(Collider other)
	{
		if (ActiveMovementController == null) return;
		if (!ActiveMovementController.activeGame) return;
		if (other.transform.parent.name == "LeftHand (Teleport Locomotion)" & name == "Left")
			ActiveMovementController.CurrentPoints++;
		if (other.transform.parent.name == "RightHand (Teleport Locomotion)" & name == "Right")
			ActiveMovementController.CurrentPoints++;
	}
}