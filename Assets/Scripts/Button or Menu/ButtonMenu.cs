using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    TawanTitle tawanRun;

    private void Start()
    {
        tawanRun = FindObjectOfType<TawanTitle>();
    }
    public void OnButtonPlay()
    {
        //SceneManager.LoadScene("SceneOne");
        tawanRun.StartMoving();
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
