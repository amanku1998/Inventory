using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoHandler : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private TMP_Text itemPrice;
    [SerializeField] private TMP_Text itemWeight;
    [SerializeField] private TMP_Text itemQuantity;
    [SerializeField] private TMP_Text itemRarity;
    [SerializeField] private TMP_Text setQuantity;
    [SerializeField] private TMP_Text totalPrice;
    private ItemSlot selectedSlot;
    private Items selectedInventoryItem;
    private int currentQuantity = 1;

    private void OnEnable()
    {
        GameService.Instance.GetEventService().OnSlotSelect.AddListener(OpenInventoryItemInfoPopup);
    }
    private void OnDisable()
    {
        GameService.Instance.GetEventService().OnSlotSelect.RemoveListener(OpenInventoryItemInfoPopup);
        if (selectedSlot) selectedSlot.DisableSelectionBox();
    }

    public void OpenInventoryItemInfoPopup(ItemSlot slot)
    {
        if (slot.GetInventoryItem() == null && slot.GetItemQuantity() <= 0) return;
        if (selectedSlot) selectedSlot.DisableSelectionBox();

        selectedSlot = slot;
        selectedInventoryItem = slot.GetInventoryItem();

        if (selectedSlot)
        {
            currentQuantity = 1;
            int price = (selectedSlot.GetSlotType() != SlotType.Inventory) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice;
            itemImage.sprite = selectedInventoryItem.itemSprite;
            itemName.SetText(selectedInventoryItem.Name.ToString());
            itemDescription.SetText(selectedInventoryItem.itemDescription);
            itemPrice.SetText(price.ToString());
            itemWeight.SetText(selectedInventoryItem.weight.ToString());
            itemQuantity.SetText(selectedSlot.GetItemQuantity().ToString());
            itemRarity.SetText(selectedInventoryItem.rarity.ToString());
            setQuantity.SetText(currentQuantity.ToString());
            totalPrice.SetText(price.ToString());
            selectedSlot.EnableSelectionBox();
        }

        GameService.Instance.GetAudioService().Play(SoundType.SelectSlot);
    }

    public void DecreaseItemQuantity()
    {
        if (selectedInventoryItem && currentQuantity > 1 && selectedSlot.GetItemQuantity() > 0)
        {
            int price = (selectedSlot.GetSlotType() != SlotType.Inventory) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice;
            GameService.Instance.GetAudioService().PlayClickSound();
            currentQuantity--;
            setQuantity.SetText(currentQuantity.ToString());
            totalPrice.SetText((price * currentQuantity).ToString());
        }
    }

    public void IncreaseItemQuantity()
    {
        if (selectedInventoryItem && currentQuantity < selectedSlot.GetItemQuantity() && selectedSlot.GetItemQuantity() > 0)
        {
            int price = (selectedSlot.GetSlotType() != SlotType.Inventory) ? selectedInventoryItem.buyingPrice : selectedInventoryItem.sellingPrice;
            GameService.Instance.GetAudioService().PlayClickSound();
            currentQuantity++;
            setQuantity.SetText(currentQuantity.ToString());
            totalPrice.SetText((price * currentQuantity).ToString());
        }
    }

    public void OnBuyItem()
    {
        GameService.Instance.GetEventService().OnBuyItem.InvokeEvent(selectedSlot, currentQuantity);
        GameService.Instance.GetAudioService().PlayClickSound();
    }

    public void OnSellItem()
    {
        GameService.Instance.GetEventService().OnSellItem.InvokeEvent(selectedSlot, currentQuantity);
        GameService.Instance.GetAudioService().PlayClickSound();
    }
}
