using UnityEngine;

public class Basket : MonoBehaviour
{
	private AppleController _appleController;
	void Start()
	{
		_appleController = FindObjectOfType<AppleController>();	
	}
	void OnTriggerEnter(Collider other)
	{
		//try to get apple script from other object
		var apple = other.GetComponent<Apple>();
		if (apple != null)
		{
			//destroy apple
			Destroy(apple.gameObject);
			//respawn apple
			_appleController.RespawnApple();
			//add point
			_appleController.CurrentPoints++;

			if (_appleController.CurrentPoints < _appleController.MaxPoints) return;
			_appleController.ActiveGame = false;
			_appleController.CurrentPoints = 0;
			var sportPoints = FindFirstObjectByType<SportPoints>();
			sportPoints.SwitchGame();
		}
	}
}
