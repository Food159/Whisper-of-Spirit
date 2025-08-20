using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHappyState : BossState
{
    public AnimationClip animclip;
    public CapsuleCollider2D col2d;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        col2d.enabled = false;
    }
    public override void Exit()
    {

    }
}
