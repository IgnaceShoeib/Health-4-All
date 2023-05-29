using System.Collections.Generic;
using UnityEngine;

public class SportPoints : MonoBehaviour
{
    private int points = 0;
    private int maxPoints = 10;
    public List<SportGame> sportGames = new List<SportGame>();

    public void Start()
    {
        var index = Random.Range(0, sportGames.Count);
        sportGames[index].ActiveGame = true;
    }

    public void SwitchGame()
    {
	    points++;
		var index = Random.Range(0, sportGames.Count);
        sportGames[index].ActiveGame = true;
	}
}
