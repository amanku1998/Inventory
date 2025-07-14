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
    private int currentWeight;

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
        shopService = new ShopService(shopView, itemDataList);
        inventoryService = new InventoryService(inventoryView, itemDataList);
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

        eventService.OnAddRandomItems.InvokeEvent(item, 1);
    }

    public void UpdateWeight(int weight)
    {
        currentWeight += weight;
        currentItemWeightText.text = $"{currentWeight}/{maxWeight}";
    }

    public Transform GetParentTransformOfUICanvas()
    {
        return uiCanvasTransform;
    }

    public EventService GetEventService()
    {
        return eventService;
    }

}
