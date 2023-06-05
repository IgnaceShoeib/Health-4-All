public class PunchingPadController : SportGame
{
	private string lastHit = "";
	public PunchingPad[] pads;

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
    public override void OnActiveGameTrue()
    {
        foreach (var pad in pads)
        {
			pad.gameObject.SetActive(true);
        }
    }
    public override void OnActiveGameFalse()
    {
        foreach (var pad in pads)
        {
            pad.gameObject.SetActive(false);
        }
    }
}