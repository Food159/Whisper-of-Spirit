using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{

    public void OnButtonPlay()
    {
        SceneManager.LoadScene("SceneOne");
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene("SceneMenu");
    }
    public void OnButtonGameOne()
    {
        SceneManager.LoadScene("SceneGameOne");
    }
    public void ExitButton()
    {
        Application.Quit();
        print("Quit");
    }
}
