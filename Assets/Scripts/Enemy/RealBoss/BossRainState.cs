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
    public BossPhase phase;

    [Header("Rain Settings")]
    public float rainInterval;
    [SerializeField] private int rainAmount = 5;
    private void Awake()
    {
        objectpool = FindObjectOfType<ObjectPool>();
    }
    public override void Enter()
    {
        if(phase == BossPhase.phase1)
        {
            rainInterval = 0.2f;
        }
        else if( phase == BossPhase.phase2)
        {
            rainInterval = 0.1f;
        }    
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
            float randomP = Random.Range(-14f, 12f);
            Vector2 spawnPos = new Vector2(randomP, 11.28f);
            //Transform rainpos = RainPoint[Random.Range(0, RainPoint.Length)];
            GameObject rainspawn = objectpool.GetBossRainObject();
            rainspawn.transform.position = spawnPos;
            rainspawn.SetActive(true);
        }

    }
    IEnumerator Rain()
    {
        float rainDuration = Random.Range(9f, 20f); // เวลาฝนตกสุ่ม 9-20 วิ
        float timer = 0f;

        while (timer < rainDuration)
        {
            for (int i = 0; i < rainAmount; i++)
            {
                float randomP = Random.Range(-14f, 12f);
                Vector2 spawnPos = new Vector2(randomP, 11.28f);
                GameObject rainspawn = objectpool.GetBossRainObject();
                rainspawn.transform.position = spawnPos;
                rainspawn.SetActive(true);
            }

            float intervalTimer = 0f;
            while (intervalTimer < rainInterval) // phase 1 interval 0.2 phase 2 0.1
            {
                intervalTimer += Time.deltaTime;
                timer += Time.deltaTime;
                yield return null;
            }
        }

        raining = false;
        Exit();
        bossInput.canShoot = false;
        yield return StartCoroutine(bossInput.DelayBeforeFire());
    }
}
