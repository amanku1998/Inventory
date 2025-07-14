using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private List<SlotList> slotLists;
    [SerializeField] private ShopController shopController;

    public List<SlotList> GetSlotLists()
    {
        return slotLists;
    }

    public void SetShopController(ShopController shopController)
    {
        this.shopController = shopController;
    }
}
