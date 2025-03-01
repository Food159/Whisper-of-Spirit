using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyhealth = collision.GetComponent<EnemyHealth>();
            if (enemyhealth != null)
            {
                enemyhealth.TakeDamage(20);
                //Destroy(collision.gameObject);
                //
            }
            Destroy(gameObject);
        }
    }
}
