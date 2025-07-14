using System.Collections.Generic;
using UnityEngine;

public class ShopController
{
    private ItemDataList itemDataList;
    private ShopView shopView;
    private ItemSlot itemSlot;
    private int quantity;
    private Items item;

    public ShopController(ShopView shopPrefab, ItemDataList itemDataList)
    {
        this.shopView = Object.Instantiate(shopPrefab);
        shopView.transform.SetParent(GameService.Instance.GetParentTransformOfUICanvas().transform, false);
        shopView.transform.SetAsFirstSibling();
        shopView.SetShopController(this);
        this.itemDataList = itemDataList;
        UpdateShop();
    }

    private void UpdateShop()
    {
        for (int i = 0; i < 4; i++)
        {
            List<Items> itemList = itemDataList.itemDataListOfScriptableObject[i].itemsList;
            List<ItemSlot> slotList = GetSlotLists((SlotType)i);
            for (int j = 0; j < slotList.Count; j++)
            {
                slotList[j].AddedNewItemInSlot(itemList[j], itemList[j].quantity);
            }
        }
    }

    public List<ItemSlot> GetSlotLists(SlotType slotType)
    {
        List<ItemSlot> SlotList = shopView.GetSlotLists().Find(list => list.slotType == slotType).slotList;

        return SlotList;
    }

}
