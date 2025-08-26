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
    [SerializeField] Transform[] RainPoint;
    private ObjectPool objectpool;

    [Header("Rain Settings")]
    [SerializeField] private float rainInterval = 0.5f;
    [SerializeField] private int rainAmount = 5;
    private void Awake()
    {
        objectpool = FindObjectOfType<ObjectPool>();
    }
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
    public void BossRain()
    {
        for (int i = 0; i < rainAmount; i++)
        {
            Transform rainpos = RainPoint[Random.Range(0, RainPoint.Length)];
            GameObject rainspawn = objectpool.GetBossRainObject();
            rainspawn.transform.position = rainpos.position;
            rainspawn.SetActive(true);
        }

    }
    IEnumerator Rain()
    {
        float rainDuration = 15f;
        float timer = 0f;
        while(timer < rainDuration) 
        {
            BossRain();
            yield return new WaitForSeconds(rainInterval);
            timer += rainInterval;
        }
        raining = false;
        Exit();
        bossInput.canShoot = false;
        yield return StartCoroutine(bossInput.DelayBeforeFire());
    }
    //IEnumerator Rain()
    //{
    //    yield return new WaitForSeconds(15f);
    //    raining = false;
    //    Exit();
    //    bossInput.canShoot = false;
    //    yield return StartCoroutine(bossInput.DelayBeforeFire());
    //}
}
