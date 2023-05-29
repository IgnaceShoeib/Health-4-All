public class PunchingPadController : SportGame
{
	private string lastHit = "";

	public void Hit(string handName)
	{
		if (!ActiveGame)
			return;
		if (lastHit == handName) return;
		lastHit = handName;
		CurrentPoints++;

		if (CurrentPoints < MaxPoints) return;
		ActiveGame = false;
		CurrentPoints = 0;
		var sportPoints = FindFirstObjectByType<SportPoints>();
		sportPoints.SwitchGame();
	}
}