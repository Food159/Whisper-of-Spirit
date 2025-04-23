using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungAttackState : LungState
{
    public AnimationClip animclip;
    bool _canAttack = true;
    //float attackCooldown = 2f;
    bool _isAnimationFinished = false;
    [SerializeField]  bool _isAttacking;
    [SerializeField] int damage;
    [SerializeField] Transform attackpoint;
    [SerializeField] float attackrange;
    [SerializeField] LayerMask playerlayer;
    PlayerHealth playerstatus;
    private void Awake()
    {
        playerstatus = FindAnyObjectByType<PlayerHealth>();
    }
    public override void Enter()
    {
        if (_canAttack && playerstatus._isPlayerDead == false)
        {
            anim.Play(animclip.name);
        }
    }
    void attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, playerlayer);
        foreach (Collider2D hit in hits) 
        {
            PlayerHealth playerhealth = hit.GetComponent<PlayerHealth>();
            PlayerController playercontroller = hit.GetComponent<PlayerController>();
            if (playerhealth != null) 
            {
                playerhealth.TakeDamage(damage);
                Debug.Log("โจมตี Player! HP ที่เหลือ: " + playerhealth.currentHealth);
            }
            if (playercontroller != null)
            {
                playercontroller.knockback(transform);
            }
        }
    }
    //IEnumerator AttackWithCooldown()
    //{
    //    _canAttack = false;
    //    attack();
    //    yield return new WaitForSeconds(attackCooldown);
    //    _canAttack = true;
    //}
    public override void Do()
    {
        rb2d.velocity = Vector2.down;
        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
        if(stateinfo.normalizedTime >= 1f)
        {
            _isAnimationFinished = true;
        }
        if(linput.DistanceCal() > 1.9f && _isAnimationFinished)
        {
            isComplete = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }
    public override void Exit()
    {

    }
}
