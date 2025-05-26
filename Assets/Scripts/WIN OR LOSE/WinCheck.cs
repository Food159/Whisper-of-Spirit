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
    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        playercontroller = FindObjectOfType<PlayerController>();
        playershooting = FindObjectOfType<PlayerShooting>();
    }
    private void Start()
    {
        EnablePlayercontrol();
    }
    private void Update()
    {
        CheckWin();
    }
    void CheckWin()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        bool allDead = true;
        foreach(EnemyHealth enemy in enemies) 
        {
            if(!enemy._isDead)
            {
                allDead = false;
                break;
            }
        }
        if(allDead)
        {
            Win();
        }
    }
    public void Win()
    {
        if(winpanel != null) 
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
