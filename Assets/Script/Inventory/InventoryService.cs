using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryService : MonoBehaviour
{
    private InventoryController inventoryController;

    public InventoryService(InventoryView inventoryView, ItemDataList itemDataList)
    {
        inventoryController = new InventoryController(inventoryView, itemDataList);
    }
}
