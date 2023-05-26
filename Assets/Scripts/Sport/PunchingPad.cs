using UnityEngine;

public class PunchingPad : MonoBehaviour
{
	public string HandObjectName;
	private PunchingPadController punchingPadController;
	void Start()
	{
		punchingPadController = GetComponentInParent<PunchingPadController>();
		name = gameObject.name;
	}
	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.transform.parent.name);
		if (other.transform.parent.name == HandObjectName)
			punchingPadController.Hit(name);
	}
}
