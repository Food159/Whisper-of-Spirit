using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaintState : State
{
    public AnimationClip animclip;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {

    }
    public override void FixedDo()
    {

    }
    public override void Exit()
    {

    }
}
