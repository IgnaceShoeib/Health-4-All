using UnityEngine;

public class Barbell : MonoBehaviour
{
	public float down = 0.25f;
    public float up = 1.5f;
    public int amount = 15;

    public int currentAmount = 0;
    private bool isUp = false;
    private bool addedAmount = false;
    // Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
		    currentAmount++;
		    addedAmount = true;
	    }
	    else if (!isUp)
	    {
		    addedAmount = false;
	    }
    }
}
