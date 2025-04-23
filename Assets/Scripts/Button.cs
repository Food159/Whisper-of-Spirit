using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    WinCheck wincheck;
    private void Start()
    {
        wincheck = GetComponent<WinCheck>();
    }
    public void OnButtonRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene("SceneMenu");
        //wincheck.wintestCheck = false;
        Time.timeScale = 1;
        wincheck.winpanel.SetActive(false);
        wincheck.wintestCheck = false;
        Time.timeScale = 1;
    }
}
