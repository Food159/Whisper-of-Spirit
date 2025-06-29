using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    private float timer;
    private void OnEnable()
    {
        timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            EnemyHealth enemyhealth = collision.GetComponent<EnemyHealth>();
            if (enemyhealth != null)
            {
                enemyhealth.TakeDamage(20);
            }
            gameObject.SetActive(false);
        }
    }
}
