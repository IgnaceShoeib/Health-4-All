using UnityEngine;

public class Barbell : SportGame
{
	public float down = 0.25f;
    public float up = 1.5f;
	
    private bool isUp = false;
    private bool addedAmount = false;

    // Update is called once per frame
    void Update()
    {
		if (!ActiveGame)
			return;
	    if (transform.position.y >= up)
	    {
		    isUp = true;
	    }
	    else if (transform.position.y <= down)
	    {
		    isUp = false;
	    }

	    if (isUp && !addedAmount)
	    {
		    CurrentPoints++;
		    addedAmount = true;
	    }
	    else if (!isUp)
	    {
		    addedAmount = false;
	    }

	    if (CurrentPoints < MaxPoints) return;
	    ActiveGame = false;
	    CurrentPoints = 0;
	    var sportPoints = FindFirstObjectByType<SportPoints>();
	    sportPoints.SwitchGame();
	}
}
