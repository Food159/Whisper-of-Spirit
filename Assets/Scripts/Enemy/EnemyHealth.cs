using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool _isDead;
    public HealthBar healthbar;
    //public Transform healthbarpos;
    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        //if (healthbar != null && healthbarpos != null)
        //{
        //    healthbar.transform.position = healthbarpos.position;
        //}
    }
    public void TakeDamage(int damage)
    {
        if (_isDead)
            return;

        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            _isDead = true;
        }
    }
}
