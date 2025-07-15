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

    public void OnEnable()
    {
        GameService.Instance.GetEventService().OnBuyItem.AddListener(SetBuyItemInfo);
        //GameService.Instance.GetEventService().OnConfirmSellItem.AddListener(AddItemInShop);
    }

    public void OnDisable()
    {
        GameService.Instance.GetEventService().OnBuyItem.RemoveListener(SetBuyItemInfo);
        //GameService.Instance.GetEventService().OnConfirmSellItem.RemoveListener(AddItemInShop);
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

    private void ResetSlot()
    {
        itemSlot = null;
        quantity = 0;
        item = null;
    }

    private void SetBuyItemInfo(ItemSlot Slot, int Quantity)
    {
        itemSlot = Slot;
        quantity = Quantity;
        item = Slot.GetInventoryItem();
    }

    public void BuyConfirm()
    {
        if (itemSlot && itemSlot.GetSlotType() != SlotType.Inventory)
        {
            int totalCost = item.buyingPrice * quantity;
            int totalWeight = item.weight * quantity;

            if (totalCost > GameService.Instance.GetCoin())
            {
                GameService.Instance.GetEventService().OnBuyFailed.InvokeEvent(BuyFailedType.Coin);
                ResetSlot();
                return;
            }

            if (totalWeight > GameService.Instance.GetRemainingWeight())
            {
                GameService.Instance.GetEventService().OnBuyFailed.InvokeEvent(BuyFailedType.InventoryWeight);
                ResetSlot();
                return;
            }

            GameService.Instance.SpentCoin(totalCost);
            itemSlot.DecreaseItemQuantity(quantity);

            GameService.Instance.GetEventService().OnConfirmBuyItem.InvokeEvent(item, quantity);
            ResetSlot();
        }
    }
}
