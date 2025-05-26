using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Subject
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool _isPlayerDead = false;
    public HealthBar healthbar;
    [SerializeField] GameObject Losepanel;

    [Header("Sprite")]
    public Image tawanimage;
    public Image daraimage;

    public Sprite TawanHappySprite;
    public Sprite TawanFineSprite;
    public Sprite TawanBadSprite;

    public Sprite DaraHappySprite;
    public Sprite DaraFineSprite;
    public Sprite DaraBadSprite;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        if (_isPlayerDead)
        {
            StartCoroutine(LoseAfterDelay());
        }
    }
    public void TakeDamage(int damage)
    {
        if (_isPlayerDead)
            return;

        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        UpdateSprite();
        if (currentHealth <= 0)
        {
            _isPlayerDead = true;
        }
    }
    IEnumerator LoseAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        Losepanel.SetActive(true);
    }
    void UpdateSprite()
    {
        if (currentHealth <= 20) 
        {
            tawanimage.sprite = TawanBadSprite;
            daraimage.sprite = DaraBadSprite;
        }
        else if (currentHealth <= 60)
        {
            tawanimage.sprite = TawanFineSprite;
            daraimage.sprite = DaraFineSprite;
        }
        else
        {
            tawanimage.sprite = TawanHappySprite;
            daraimage.sprite = DaraHappySprite;
        }
    }
}
