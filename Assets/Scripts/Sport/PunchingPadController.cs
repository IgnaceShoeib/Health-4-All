using UnityEngine;

public class PunchingPadController : MonoBehaviour
{
	private string lastHit = "";
	public int points;
	public int MaxPoints = 10;

	public void Hit(string handName)
	{
		if (lastHit == handName) return;
		lastHit = handName;
		points++;
	}
}
