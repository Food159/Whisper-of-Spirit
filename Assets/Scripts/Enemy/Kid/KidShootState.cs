using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class KidShootState : KidState
{
    public AnimationClip animclip;
    bool _canAttack = true;
    bool _isAnimationFinished = false;
    [SerializeField] bool _isAttacking;
    [SerializeField] LayerMask playerlayer;
    PlayerHealth playerstatus;
    private ObjectPool objectPool;

    [Header("Variable")]
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 5f;
    private void Awake()
    {
        playerstatus = FindAnyObjectByType<PlayerHealth>();
        objectPool = FindObjectOfType<ObjectPool>();
    }
    public override void Enter()
    {
        if (_canAttack && playerstatus._isPlayerDead == false)
        {
            anim.Play(animclip.name);
            KShoot();
        }
    }
    void attack()
    {
        {
        //Collider2D[] hits = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, playerlayer);
        //foreach (Collider2D hit in hits)
        //{
        //    SoundManager.instance.PlaySfx(SoundManager.instance.lungAttackClip);
        //    PlayerHealth playerhealth = hit.GetComponent<PlayerHealth>();
        //    PlayerController playercontroller = hit.GetComponent<PlayerController>();
        //    if (playerhealth != null)
        //    {
        //        playerhealth.TakeDamage(damage);
        //        Debug.Log("ยิงโดน Player! HP ที่เหลือ: " + playerhealth.currentHealth);
        //    }
            //if (playercontroller != null)
            //{
            //    playercontroller.knockback(transform);
            //}
        }
    }
    void KShoot()
    {
        if(objectPool == null && firePoint == null)
            return;

        GameObject bullet = objectPool.GetObject();
        if(bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
            if(rb2d != null)
            {
                float direction;
                if(transform.localScale.x > 0f)
                {
                    direction = 1f;
                }
                else
                {
                    direction = -1f;
                }
                rb2d.velocity = new Vector2(direction * bulletSpeed, 0f);
            }
        }
    }
    public override void Do()
    {

    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }
    public override void Exit()
    {

    }
}
