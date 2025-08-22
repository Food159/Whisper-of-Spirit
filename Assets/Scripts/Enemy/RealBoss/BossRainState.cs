using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRainState : BossState
{
    public AnimationClip animclip;
    public CapsuleCollider2D col2d;
    public bool raining;
    public Transform bossRainPos;
    public float timeCount;
    public override void Enter()
    {
        anim.Play(animclip.name);
        col2d.enabled = false;
        transform.position = bossRainPos.position;
        raining = true;
        timeCount = 0;
        
        StartCoroutine(Rain());
    }
    public override void Do()
    {
        if(raining)
        {
            timeCount += Time.deltaTime;
        }
    }
    public override void Exit()
    {
        transform.position = bossInput.startBossPos.position;
        col2d.enabled = true;
    }
    IEnumerator Rain()
    {
        yield return new WaitForSeconds(15f);
        raining = false;
        Exit();
        bossInput.canShoot = false;
        yield return StartCoroutine(bossInput.DelayBeforeFire());
    }
}
