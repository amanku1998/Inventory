using UnityEngine;

public class InventoryController
{
    [SerializeField] private ItemDataList itemDataList;

    private ItemSlot slot;
    private int quantity;
    private Items item;

    private InventoryView inventoryView;

    public void OnEnable()
    {
       GameService.Instance.GetEventService().OnAddRandomItems.AddListener(AddItemInInventory);
       GameService.Instance.GetEventService().OnConfirmBuyItem.AddListener(AddItemInInventory);
       GameService.Instance.GetEventService().OnSellItem.AddListener(SetSellItemInfo);
    }

    public void OnDisable()
    {
        GameService.Instance.GetEventService().OnAddRandomItems.RemoveListener(AddItemInInventory);
        GameService.Instance.GetEventService().OnConfirmBuyItem.RemoveListener(AddItemInInventory);
        GameService.Instance.GetEventService().OnSellItem.RemoveListener(SetSellItemInfo);
    }

    public InventoryController(InventoryView inventoryView, ItemDataList itemDataList)
    {
        this.inventoryView = Object.Instantiate(inventoryView);
        this.inventoryView.transform.SetParent(GameService.Instance.GetParentTransformOfUICanvas(), false);
        this.inventoryView.transform.SetAsFirstSibling();
        this.inventoryView.SetInventoryController(this);
        this.itemDataList = itemDataList;
    }

    private void AddItemInInventory(Items inventoryItem, int quantity)
    {
        for (int i = 0; i < inventoryView.GetSlotLists().Count; i++)
        {
            ItemSlot slot = inventoryView.GetSlotLists()[i];

            if (slot.GetInventoryItem() == null)
            {
                slot.AddedNewItemInSlot(inventoryItem, quantity);
                break;
            }
            else
            if (slot.GetInventoryItem().Name == inventoryItem.Name)
            {
                slot.AddedSameItemInSlot(quantity);
                break;
            }
        }

        int totalWeightGain = inventoryItem.weight * quantity;
        GameService.Instance.UpdateWeight(totalWeightGain);
    }

    private void SetSellItemInfo(ItemSlot itemSlot, int Quanitity)
    {
        slot = itemSlot;
        quantity = Quanitity;
        item = slot.GetInventoryItem();
    }

    public void SellConfirm()
    {
        if (slot && slot.GetSlotType() == SlotType.Inventory)
        {
            int totalCost = item.sellingPrice * quantity;
            int totalWeightDec = item.weight * quantity;

            slot.DecreaseItemQuantity(quantity);

            GameService.Instance.AddCoin(totalCost);
            GameService.Instance.GetAudioService().Play(SoundType.SellSuccess);
            GameService.Instance.GetEventService().OnConfirmSellItem.InvokeEvent(item, quantity);
            GameService.Instance.UpdateWeight(-totalWeightDec);
            slot = null;
            quantity = 0;
        }
    }
}
