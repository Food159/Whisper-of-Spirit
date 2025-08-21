using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRainState : BossState
{
    public AnimationClip animclip;
    public CapsuleCollider2D col2d;
    public bool raining;
    public override void Enter()
    {
        anim.Play(animclip.name);
        col2d.enabled = false;
        transform.position = new Vector2(-0.85f, 6.52f);
        raining = true;
        StartCoroutine(Rain());
    }
    public override void Do()
    {
        
    }
    public override void Exit()
    {
        col2d.enabled = true;
    }
    IEnumerator Rain()
    {
        yield return new WaitForSeconds(15f);
        raining = false;
        Exit();
    }
}
