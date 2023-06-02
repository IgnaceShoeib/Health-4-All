using UnityEngine;

public class SportGame : MonoBehaviour
{
	public bool activeGame = false;
	public bool ActiveGame
	{
		get => activeGame;
		set
		{
			activeGame = value;
			if (activeGame)
				OnActiveGameTrue();
			else
				OnActiveGameFalse();
		}
	}
    public int MaxPoints = 10;
    public int CurrentPoints = 0;

    public virtual void OnActiveGameTrue()
    {

    }
    public virtual void OnActiveGameFalse()
    {

    }
}
