using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    public bool wintestCheck = false;
    [SerializeField] public GameObject winpanel;
    [SerializeField] public GameObject losepanel;
    PauseMenu pauseMenu;
    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
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
        Time.timeScale = 0f;
    }
}
