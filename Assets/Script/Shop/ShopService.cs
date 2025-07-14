using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopService
{
    private ShopController shopController;

    public ShopService(ShopView shopView, ItemDataList itemDataList)
    {
        shopController = new ShopController(shopView, itemDataList);
    }

    public ShopController GetShopController()
    {
        return shopController;
    }
}
