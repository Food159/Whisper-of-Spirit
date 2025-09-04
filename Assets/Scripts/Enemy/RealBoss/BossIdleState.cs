using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    public AnimationClip animclip;
    public override void Enter()
    {
        Debug.Log("Idle");
        anim.Play(animclip.name);
    }
    public override void Do()
    {

    }
    public override void Exit()
    {

    }
}
