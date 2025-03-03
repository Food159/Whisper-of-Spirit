using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void OnButtonRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnButtonMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SceneMenu");
    }
}
