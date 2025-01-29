using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textNames;
    public TextMeshProUGUI textContinue;
    [TextArea(3,10)]
    public string[] lines;
    public string[] names;
    public float textSpeed;

    private int index;
    private void Start()
    {
        textComponent.text = string.Empty;
        textNames.text = string.Empty;
        StartDialog();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index] && textNames.text == names[index]) 
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                textNames.text = names[index];
            }
        }
        if(textComponent.text == lines[index])
        {
            textContinue.gameObject.SetActive(true);
        }
        else
        {
            textContinue.gameObject.SetActive(false);
        }
    }

    void StartDialog()
    {
        index = 0;
        UpdateName();
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        textNames.text = string.Empty;
        int maxLength = Mathf.Max(names[index].Length, lines[index].Length);

        for(int i = 0; i < maxLength; i++) 
        {
            if(i < names[index].Length)
            {
                textNames.text += names[index][i];
            }
            if(i < lines[index].Length) 
            {
                textComponent.text += lines[index][i];
            }
            yield return new WaitForSeconds(textSpeed);
        }
        //foreach (char c in names[index].ToCharArray())
        //{
        //    textNames.text += c;
        //    yield return new WaitForSeconds(textSpeed);
        //}
        //foreach (char c in lines[index].ToCharArray()) 
        //{
        //    textComponent.text += c;
        //    yield return new WaitForSeconds(textSpeed);
        //}
        
    }
    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            UpdateName();
            StartCoroutine(TypeLine());
        }
        else
        {
            //gameObject.SetActive(false);
            SceneManager.LoadScene("SceneGameOne");
        }
    }
    void UpdateName()
    {
        if(index < names.Length) 
        {
            textNames.text = names[index];
        }
        else 
        {
            textNames.text = string.Empty;
        }
    }
}
