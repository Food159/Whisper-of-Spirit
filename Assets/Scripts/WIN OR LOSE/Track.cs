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
    private void Update()
    {
        if (winCheck == null) return;
        int dead = winCheck.DeadEnemiesCount;
        int total = winCheck.TotalEnemies;
        trackText.text = $"Enemies Defeated: {dead}/{total}";
        if(dead >= total)
        {
            trackCheck.sprite = trackComplete;
        }
        else
        {
            trackCheck.sprite = trackIncomplete;
        }
    }
}
