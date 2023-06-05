using UnityEngine;

public class PunchingPad : MonoBehaviour
{
	public string HandObjectName;
	private PunchingPadController punchingPadController;
	void Start()
	{
		punchingPadController = FindAnyObjectByType<PunchingPadController>();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.name == HandObjectName)
			punchingPadController.Hit(name);
	}
}
