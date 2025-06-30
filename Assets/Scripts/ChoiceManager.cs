using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    public Dialog dialog;
    public void OnButtonNo()
    {
        if(dialog.Choice_1.activeSelf)
        {
            dialog.Choice_1.SetActive(false);
            dialog.Choice_2.SetActive(true);
        }
        else if(dialog.Choice_2.activeSelf) 
        {
            dialog.Choice_2.SetActive(false);
            dialog.Choice_3.SetActive(true);
        }
        else
        {
            dialog.Choice_3.SetActive(false);
            //SceneManager.LoadScene("SceneMenu");
            SceneController.instance.LoadSceneName("SceneMenu");
        }
    }
    public void OnButtonYes() 
    {
        //SceneManager.LoadScene("SceneGameOne");
        SceneController.instance.LoadSceneName("SceneGameOne");
    }
}
