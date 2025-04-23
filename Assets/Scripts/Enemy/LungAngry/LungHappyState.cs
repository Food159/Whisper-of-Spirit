using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungHappyState : LungState
{
    public AnimationClip animclip;
    public CapsuleCollider2D col2d;
    public GameObject enemy;
    public GameObject hpbar;
    public GameObject guman;
    public override void Enter()
    {
        anim.Play(animclip.name);
    }
    public override void Do()
    {
        col2d.enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        Destroy(enemy, 5f);
        hpbar.SetActive(false);
        guman.SetActive(false);
    }
    public override void Exit()
    {

    }
}
