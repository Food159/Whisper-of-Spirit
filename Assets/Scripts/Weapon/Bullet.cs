using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    private float timer;
    private int damage = 510;
    private void OnEnable()
    {
        timer = 0f;
    }
    private void Update()
    {
        Vector2 bulletWorldToViewportPos = Camera.main.WorldToViewportPoint(transform.position);
        bool _isOutOfScreen = bulletWorldToViewportPos.x < 0 || bulletWorldToViewportPos.x > 1 || bulletWorldToViewportPos.y < 0 || bulletWorldToViewportPos.y > 1;

        timer += Time.deltaTime;
        if(timer >= lifeTime || _isOutOfScreen)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("Collided with: " + collision.gameObject.name);
            EnemyHealth enemyhealth = collision.GetComponent<EnemyHealth>();
            if (enemyhealth != null)
            {
                enemyhealth.TakeDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }
}
