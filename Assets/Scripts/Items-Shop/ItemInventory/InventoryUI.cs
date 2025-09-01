using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] List<Image> slots;
    [SerializeField] Sprite emptySprite;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Item 1");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Item 2");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Item 3");

        }
    }
    public bool AddItems(Sprite itemSprite)
    {
        foreach(Image slot in slots) 
        {
            if(slot.sprite == null)
            {
                slot.sprite = itemSprite;
                return true;
            }
        }
        Debug.Log("Items Full");
        return false;
    }
    public void RemoveItems(int index) 
    {
        if(index >= 0 && index < slots.Count) 
        {
            slots[index].sprite = emptySprite;
        }
    }
}
