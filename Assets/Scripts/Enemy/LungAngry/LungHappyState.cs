using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LungHappyState : LungState
{
    public AnimationClip animclip;
    public CapsuleCollider2D col2d;
    public GameObject enemy;
    public GameObject hpbar;
    public GameObject guman;

    public GameObject purifyPrefab;
    public bool _isPurify = false;
    public override void Enter()
    {
        if(!_isPurify)
        {
            GameObject prefab = Instantiate(purifyPrefab);
            prefab.transform.position = this.transform.position;
            Destroy(prefab, 0.5f);
            _isPurify = true;
        }
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
