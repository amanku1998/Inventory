using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private TextMeshProUGUI currentCoins;
    [SerializeField] private TextMeshProUGUI currentItemWeightText;
    [SerializeField] private int maxWeight = 500;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioScriptableObject audioScriptableObject;
    private AudioService audioService;

    private int coin;
    private int currentWeight;

    [SerializeField] private ItemInfoPopupHandler infoPanelHandler;
    [SerializeField] private DisplayOverlayTextHandler displayOverlayTextHandler;
    [SerializeField] private Button addRandomItems;

    [SerializeField] private Transform uiCanvasTransform;

    [SerializeField] private ItemDataList itemDataList;

    [SerializeField] private ShopView shopView;
    [SerializeField] private InventoryView inventoryView;

    private ShopService shopService;
    private InventoryService inventoryService;

    private EventService eventService;

    void Start()
    {
        eventService = new EventService();
        audioService = new AudioService(audioSource, audioScriptableObject);
        shopService = new ShopService(shopView, itemDataList);
        inventoryService = new InventoryService(inventoryView, itemDataList);
        infoPanelHandler.SubscribeEvent();
        displayOverlayTextHandler.SubscribeEvent();
        UpdateWeight(currentWeight);
    }

    public int GetCoin()
    {
        return coin;
    }

    public void AddCoin(int val)
    {
        coin += val;
        UpdateCoinText(coin);
    }

    public void SpentCoin(int itemCost)
    {
        if (coin == 0 || itemCost > coin)
        {
            return;
        }

        coin -= itemCost;
        UpdateCoinText(coin);
    }


    private void UpdateCoinText(int val)
    {
        currentCoins.text = val.ToString();
    }

    public void addRandomItemsInInventory()
    {
        ItemType randomItemType = (ItemType)Random.Range(0, itemDataList.itemDataListOfScriptableObject.Count);

        //Find the random item type(material, consumable, weapon or treasure)
        List<Items> itemList = itemDataList.itemDataListOfScriptableObject.Find(itemListData => itemListData.itemType == randomItemType).itemsList;
        //Get the random value based on no of items in the list
        int randomItem = Random.Range(0, itemList.Count);
        //get that item from the list of items
        Items item = itemList[randomItem];

        if (item.weight > GetRemainingWeight())
        {
            eventService.OnBuyFailed.InvokeEvent(BuyFailedType.InventoryWeight);
            return;
        }

        audioService.Play(SoundType.AddRandomItem);

        eventService.OnAddRandomItems.InvokeEvent(item, 1);
    }

    public void UpdateWeight(int weight)
    {
        currentWeight += weight;
        currentItemWeightText.text = $"{currentWeight}/{maxWeight}";
    }

    public int GetRemainingWeight()
    {
        return maxWeight - currentWeight;
    }

    public Transform GetParentTransformOfUICanvas()
    {
        return uiCanvasTransform;
    }

    public EventService GetEventService()
    {
        return eventService;
    }

    public AudioService GetAudioService()
    {
        return audioService;
    }
    public void ConfirmBuyItemFromShop()
    {
        shopService.GetShopController().BuyConfirm();
    }

    public void ConfirmSellItemFromInventory()
    {
        inventoryService.GetInventoryController().SellConfirm();
    }
}
