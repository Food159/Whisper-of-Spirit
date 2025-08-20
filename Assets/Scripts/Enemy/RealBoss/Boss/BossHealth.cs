using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private int maxHealth = 1000;
    public int currentHealth;
    public bool isDead;
    public HealthBar healthBar;
    private void Start()
    {
        if (isDead)
            return;
        if(currentHealth <= 0 || currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isDead) 
            return;
        currentHealth -= damage;
        SoundManager.instance.PlaySfx(SoundManager.instance.lungHit);
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }
}
