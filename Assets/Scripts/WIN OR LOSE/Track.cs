using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Track : MonoBehaviour
{
    [SerializeField] WinCheck winCheck;

    [SerializeField] Image trackCheck;
    [SerializeField] Sprite trackComplete;
    [SerializeField] Sprite trackIncomplete;
    [SerializeField] TMP_Text trackText;
    public bool trackCompleted;
    public static Track instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (winCheck == null) return;
        int dead = winCheck.DeadEnemiesCount;
        int total = winCheck.TotalEnemies;
        trackText.text = $"Enemies Defeated: {dead}/{total}";
        if(dead >= total)
        {
            trackCheck.sprite = trackComplete;
            trackCompleted = true;
        }
        else
        {
            trackCheck.sprite = trackIncomplete;
            trackCompleted = false;
        }
    }
}
