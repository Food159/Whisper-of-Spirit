using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;
public enum BossPhase
{
    phase1, phase2
}
public class BossFireState : BossState
{
    public AnimationClip animclip;
    PlayerHealth playerStatus;
    public BossPhase bossphase;
    private ObjectPool objectpool;
    private BossController bossController;

    [Space]
    [Header("Variable")]
    //private float attackSpeed = 3;

    public bool _isAttacking;
    [SerializeField] Transform[] portalPoint;
    
    private void Awake()
    {
        playerStatus = FindAnyObjectByType<PlayerHealth>();
        objectpool = FindObjectOfType<ObjectPool>();
        bossController = GetComponent<BossController>();
    }
    public override void Enter()
    {
        if (playerStatus._isPlayerDead == false)
        {
            anim.Play(animclip.name);
            
        }
    }
    public void CantShoot()
    {
        bossController.canShoot = false;
    }
    public void BossPortal()
    {
        if(bossphase == BossPhase.phase1) 
        {
            Transform allPoint = portalPoint[Random.Range(0, 6)];
            GameObject portal = objectpool.GetBossObject();
            if (portal != null)
            {
                portal.transform.position = allPoint.position;
                portal.transform.rotation = allPoint.rotation;
                BossPortal bossportal = portal.GetComponent<BossPortal>();
                if (bossportal != null)
                {
                    if (allPoint == portalPoint[0] || allPoint == portalPoint[1] || allPoint == portalPoint[2])
                    {
                        bossportal.SetDirection(true);
                    }
                    else
                    {
                        bossportal.SetDirection(false);
                    }
                }
                
            }
        }
        else if (bossphase == BossPhase.phase2)
        {
            Transform leftPoint = portalPoint[Random.Range(0, 3)];
            Transform rightPoint = portalPoint[Random.Range(3, 6)];
            GameObject leftPortal = objectpool.GetBossObject();
            if (leftPortal != null)
            {
                leftPortal.transform.position = leftPoint.position;
                leftPortal.transform.rotation = leftPoint.rotation;
                BossPortal bossportal = leftPortal.GetComponent<BossPortal>();
                if (bossportal != null)
                {
                    bossportal.SetDirection(true);
                }
            }
            GameObject rightPortal = objectpool.GetBossObject();
            if (rightPortal != null)
            {
                rightPortal.transform.position = rightPoint.position;
                rightPortal.transform.rotation = rightPoint.rotation;
                BossPortal bossportal = rightPortal.GetComponent<BossPortal>();
                if (bossportal != null)
                {
                    bossportal.SetDirection(false);
                }
            }
        }

    }
    public override void Do()
    {

    }
    public override void Exit()
    {

    }
}
