using System.Collections.Generic;
using UnityEngine;

public class MovementController : SportGame
{
	private Animator Animator;
	public Animator PlayerAnimator;
	public string animationName;
    private List<HandCollision> handCollisions;
    void Start()
    {
        handCollisions = new List<HandCollision>(FindObjectsOfType<HandCollision>());
        Animator = GetComponent<Animator>();
    }
    
    void Update()
    {
	    if (!ActiveGame) return;
		if (CurrentPoints < MaxPoints) return;
		ActiveGame = false;
	    CurrentPoints = 0;
		var sportPoints = FindFirstObjectByType<SportPoints>();
		sportPoints.SwitchGame();
	}

    public override void OnActiveGameTrue()
    {
	    Animator.SetTrigger(animationName);
	    PlayerAnimator.SetTrigger(animationName);
	    foreach (var handCollision in handCollisions)
	    {
		    handCollision.ActiveMovementController = this;
	    }
    }
    public override void OnActiveGameFalse()
    {
	    Animator.SetTrigger("Idle");
	    PlayerAnimator.SetTrigger("Idle");
    }
}
