using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    [SerializeField] GameObject winpanel;
    [SerializeField] GameObject losepanel;
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
    void Win()
    {
        if(winpanel != null) 
        {
            winpanel.SetActive(true);
        }
        Time.timeScale = 0f;
        if(pauseMenu != null)
        {
            //pauseMenu.DisablePlayercontrol();
        }
        else
        {
            Debug.Log("NoPause menu");
        }
    }
}
