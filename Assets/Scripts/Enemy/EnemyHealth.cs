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
    public Transform healthbarpos;
    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        healthbar.transform.position = healthbarpos.position;
        //if(Input.GetKeyDown(KeyCode.H))
        //{
        //    TakeDamage(30);
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
