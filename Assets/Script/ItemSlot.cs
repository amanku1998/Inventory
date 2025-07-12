using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private SlotType slotType;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image selectioxBox;
    [SerializeField] private TMP_Text itemQuantityText;
    [SerializeField] private Items items;
    private int itemQuantity;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (items && itemQuantity > 0)
        {
            itemImage.enabled = true;
            itemQuantityText.enabled = true;
            itemImage.sprite = items.itemSprite;
            itemQuantityText.text = itemQuantity.ToString();
        }
        else
        {
            ResetSlot();
        }
    }

    public void AddedNewItemInSlot(Items Inventoryitem, int quantity)
    {
        items = Inventoryitem;
        itemQuantity = quantity;
        UpdateSlot();
    }

    public void AddedSameItemInSlot(int quantity)
    {
        itemQuantity += quantity;
        UpdateSlot();
    }

    void ResetSlot()
    {
        itemQuantity = 0;
        itemImage.sprite = null;
        itemQuantityText.text = "";
        itemImage.enabled = false;
        itemQuantityText.enabled = false;
        items = null;
    }

    public Items GetInventoryItem()
    {
        return items;
    }
}

[System.Serializable]
public enum SlotType
{
    Materials,
    Weapons,
    Consumables,
    Treasure,
    Inventory
}
