using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;
    public void togglePanel()
    {
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void closePanel()
    {
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
