using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Items : ScriptableObject
{
    public ItemName Name;
    public ItemType Type;
    public Rarity rarity;
    public Sprite itemSprite;
    public string itemDescription;
    public int buyingPrice;
    public int sellingPrice;
    public int weight;
    public int quantity;
}


[System.Serializable]
public enum ItemType
{
    Materials,
    Weapons,
    Consumables,
    Treasure
}

[System.Serializable]
public enum Rarity
{
    VeryCommon,
    Common,
    Rare,
    Epic,
    Legendary
}