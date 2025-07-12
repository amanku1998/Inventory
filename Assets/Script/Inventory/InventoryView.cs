using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> itemSlotListInventory;
    [SerializeField] private InventoryController inventoryController;

    // Start is called before the first frame update
    void Start()
    {
        inventoryController.OnEnable();
    }

    public void SetInventoryController(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
    }

    public List<ItemSlot> GetSlotLists()
    {
        return itemSlotListInventory;
    }
}
