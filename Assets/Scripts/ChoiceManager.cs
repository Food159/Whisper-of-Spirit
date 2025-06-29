using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    public Dialog dialog;
    public void OnButtonNo()
    {
        dialog.Choice_1.SetActive(false);
        if(!dialog.Choice_1.activeSelf)
        {
            dialog.Choice_2.SetActive(true);
        }
        else if(!dialog.Choice_2.activeSelf) 
        {
            dialog.Choice_3.SetActive(true);
        }
        else if(!dialog.Choice_3.activeSelf)
        {
            SceneManager.LoadScene("SceneMenu");
        }
    }
    //public void OnButtonNo2() 
    //{
    //    dialog.Choice_2.SetActive(false);
    //    dialog.Choice_3.SetActive(true);
    //}
    //public void OnButtonNo3() 
    //{
    //    SceneManager.LoadScene("SceneMenu");
    //}
    public void OnButtonYes() 
    {
        SceneManager.LoadScene("SceneGameOne");
    }
}
