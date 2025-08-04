using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class KidWalkState : KidState
{
    public AnimationClip animclip;
    private float walkspeed = 1.5f;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        float direction = Mathf.Sign(kidInput.playerTarget.position.x - kidInput.transform.position.x);
        if (kidInput.transform.localScale.x > 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        rb2d.velocity = new Vector2(walkspeed * kidInput.sentDirection, rb2d.velocity.y);
        rb2d.velocity = new Vector2(walkspeed * direction, rb2d.velocity.y);
        if (kidInput.DistanceCal() <= 1.9f || kidInput.DistanceCal() > 10f || kidInput.isOutofArea)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {

    }
}
