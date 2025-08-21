using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BossPortal : MonoBehaviour
{
    private ObjectPool objectpool;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    BossFireState bossFire;
    BossController bossController;
    public bool isLeft;
    private void Awake()
    {
        objectpool = FindObjectOfType<ObjectPool>();
        bossFire = FindObjectOfType<BossFireState>();
        bossController = FindObjectOfType<BossController>();
    }
    public void ClosePortal()
    {
        gameObject.SetActive(false);
    }
    public void SetDirection(bool left)
    {
        isLeft = left;
        if(spriteRenderer != null) 
        {
            spriteRenderer.flipX = isLeft;
        }
    }
    public void BossShoot()
    {
        GameObject bossBullet = objectpool.GetBossBulletObject();
        if (bossBullet != null)
        {
            bossBullet.transform.position = transform.position;
            Rigidbody2D rb2d = bossBullet.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                if(isLeft)
                {
                    rb2d.velocity = Vector2.right * bulletSpeed;
                }
                else if(!isLeft)
                {
                    rb2d.velocity = Vector2.left * bulletSpeed;
                }
                bossController.currentAttackCount++;
            }
        }
    }
}
