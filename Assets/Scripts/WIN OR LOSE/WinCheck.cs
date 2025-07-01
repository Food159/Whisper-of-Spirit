using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    public bool wintestCheck = false;
    [SerializeField] public GameObject winpanel;
    [SerializeField] public GameObject losepanel;

    private PlayerController playercontroller;
    private PlayerShooting playershooting;
    PauseMenu pauseMenu;

    private EnemyHealth[] allEnemies;
    public int TotalEnemies => allEnemies.Length;
    public int DeadEnemiesCount { get; private set; }
    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        playercontroller = FindObjectOfType<PlayerController>();
        playershooting = FindObjectOfType<PlayerShooting>();
    }
    private void Start()
    {
        allEnemies = FindObjectsOfType<EnemyHealth>();
        EnablePlayercontrol();
    }
    private void Update()
    {
        CheckWin();
        UpdateDeadEnemies();
    }
    void UpdateDeadEnemies()
    {
        int count = 0;
        foreach(EnemyHealth enemy in allEnemies) 
        {
            if(enemy._isDead)
            {
                count++;
            }
        }
        DeadEnemiesCount = count;
    }
    void CheckWin()
    {
        if(!wintestCheck && DeadEnemiesCount >= TotalEnemies)
        {
            Win();
        }
    }
    public void Win()
    {
        wintestCheck = true;
        SoundManager.instance.PlaySfx(SoundManager.instance.winClip);
        if (winpanel != null) 
        {
            winpanel.SetActive(true);
        }
        wintestCheck = true;
        DisablePlayercontrol();
        playercontroller.CanMove();
    }
    public void DisablePlayercontrol()
    {
        if (playercontroller != null) playercontroller.enabled = false;
        if (playershooting != null) playershooting.enabled = false;
    }
    public void EnablePlayercontrol()
    {
        if (playercontroller != null) playercontroller.enabled = true;
        if (playershooting != null) playershooting.enabled = true;
    }
}
