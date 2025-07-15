using UnityEngine;
using TMPro;

public class ItemDealConfirmation : MonoBehaviour
{
    [SerializeField] private TMP_Text confirmationText;

    private void OnEnable()
    {
        GameService.Instance.GetEventService().OnBuyItem.AddListener(UpdateBuyConfirmationText);
        GameService.Instance.GetEventService().OnSellItem.AddListener(UpdateSellConfirmationText);
    }

    private void OnDisable()
    {
        GameService.Instance.GetEventService().OnBuyItem.RemoveListener(UpdateBuyConfirmationText);
        GameService.Instance.GetEventService().OnSellItem.RemoveListener(UpdateSellConfirmationText);
    }

    private void UpdateBuyConfirmationText(ItemSlot itemSlot, int quantity)
    {
        Items item = itemSlot.GetInventoryItem();
        confirmationText.text = $"Do You Want to Buy {quantity} {item.Name} for {item.buyingPrice * quantity} coins";
    }

    private void UpdateSellConfirmationText(ItemSlot itemSlot, int quantity)
    {
        Items item = itemSlot.GetInventoryItem();
        confirmationText.text = $"Do You Want to Sell {quantity} {item.Name} for {item.sellingPrice * quantity} coins";
    }
}
