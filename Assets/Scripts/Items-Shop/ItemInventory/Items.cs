using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Items : MonoBehaviour
{
    public LevelShop levelshop;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] InventoryUI inventory;
    [SerializeField] InventorySlot inventoryslot;
    //public TMP_Text timerText;
    public bool damageIncrese = false;
    private void Awake()
    {
        playerShooting = FindAnyObjectByType<PlayerShooting>();
        inventory = FindAnyObjectByType<InventoryUI>();
    }
    private void Update()
    {
        if(levelshop == LevelShop.Normal)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseItems(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseItems(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseItems(2);
            }
        }
    }
    void UseItems(int index)
    {
        ItemsType type = inventory.GetItemType(index);
        switch (type) 
        {
            case ItemsType.mango:
                StartCoroutine(UseMango(10f));
                inventory.RemoveItem(index);
                break;
            case ItemsType.rice:
                StartCoroutine(UseRice(5f));
                inventory.RemoveItem(index);
                break;
            case ItemsType.green:
                StartCoroutine(UseGreen(5f));
                inventory.RemoveItem(index);
                break;
            default:
                Debug.Log("NO ITEMS");
                break;
        }
    }
    IEnumerator UseMango(float duration)
    {
        playerShooting.bulletSpeed = 10 / 0.5f; //10 / 0.5f = 20
        playerShooting.waterReload = 1.25f * 0.5f; // 1.25 * 0.5 = 0.625
        damageIncrese = true;
        Debug.Log("UseMango");
        yield return new WaitForSeconds(10f);
        //yield return StartCoroutine(StartTimer(duration));
        playerShooting.bulletSpeed = 10f;
        playerShooting.waterReload = 1.25f;
        damageIncrese = false;
    }
    IEnumerator UseRice(float duration)
    {
        playerShooting.bulletSpeed = 10 / 0.5f;
        playerShooting.waterReload = 1.25f * 0.5f;
        Debug.Log("UseRice");
        yield return new WaitForSeconds(5f);
        //yield return StartCoroutine(StartTimer(duration));
        playerShooting.bulletSpeed = 10f;
        playerShooting.waterReload = 1.25f;
    }
    IEnumerator UseGreen(float duration)
    {
        damageIncrese = true;
        Debug.Log("UseGreen");
        yield return new WaitForSeconds(5f);
        //yield return StartCoroutine(StartTimer(duration));
        damageIncrese = false;
    }
    //IEnumerator StartTimer(float duration)
    //{
    //    float timeLeft = duration;
    //    while (timeLeft > 0)
    //    {
    //        inventoryslot.timerText.text = timeLeft.ToString("F1");
    //        timeLeft -= Time.deltaTime;
    //        yield return null;
    //    }
    //    inventoryslot.timerText.text = "0.0";
    //}
}
