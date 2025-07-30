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
        GameDataHandler.instance.LoadData();
    }
    public void OnButtonMenu()
    {
        SceneController.instance.LoadSceneName("SceneMenu");
        //wincheck.wintestCheck = false;
        Time.timeScale = 1;
    }
    public void NextLevel()
    {
        SceneController.instance.LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
        GameDataHandler.instance.LoadData();
    }
}
