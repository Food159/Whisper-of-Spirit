using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;
    public bool _isMenu;

    private PlayerController playercontroller;
    private PlayerShooting playershooting;
    private void Awake()
    {
        playercontroller = FindObjectOfType<PlayerController>();
        playershooting = FindObjectOfType<PlayerShooting>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Debug.Log("esc");
            if(_isMenu)
            {
                closePanel();
            }
            else 
            {
                togglePanel();
            }
        }
        if(!_isMenu)
        {
            EnablePlayercontrol();
        }
        else
        {
            DisablePlayercontrol();
        }
    }
    public void togglePanel()
    {
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
            _isMenu = true;
        }
    }
    public void closePanel()
    {
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
            _isMenu = false;
        }
    }
    private void DisablePlayercontrol()
    {
        if (playercontroller != null) playercontroller.enabled = false;
        if (playershooting != null) playershooting.enabled = false;
    }
    private void EnablePlayercontrol()
    {
        if (playercontroller != null) playercontroller.enabled = true;
        if (playershooting != null) playershooting.enabled = true;
    }
}
