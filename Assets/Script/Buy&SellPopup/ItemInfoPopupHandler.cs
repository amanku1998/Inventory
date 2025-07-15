using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInfoPopupHandler : MonoBehaviour
{
    [SerializeField] private ItemInfoHandler itemBuyPanel, itemSellPanel;
    [SerializeField] private GameObject ItemDealConfirmationPanel;
    [SerializeField] private TMP_Text confirmationText;

    public void SubscribeEvent()
    {
        GameService.Instance.GetEventService().OnSlotSelect.AddListener(OpenItemInfoPanel);
    }

    private void OnDisable()
    {
        GameService.Instance.GetEventService().OnSlotSelect.RemoveListener(OpenItemInfoPanel);
    }

    private void OpenItemInfoPanel(ItemSlot itemSlot)
    {
        if (itemSlot.GetInventoryItem() == null && itemSlot.GetItemQuantity() <= 0) return;
        if (!itemBuyPanel.gameObject.activeInHierarchy || !itemSellPanel.gameObject.activeInHierarchy)
        {
            if (itemSlot.GetSlotType() != SlotType.Inventory)
            {
                itemBuyPanel.gameObject.SetActive(true);
                itemBuyPanel.OpenInventoryItemInfoPopup(itemSlot);
            }
            else
            {
                itemSellPanel.gameObject.SetActive(true);
                itemSellPanel.OpenInventoryItemInfoPopup(itemSlot);
            }
        }
    }
}
