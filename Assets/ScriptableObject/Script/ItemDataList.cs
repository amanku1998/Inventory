using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDataList", menuName = "Inventory/ItemDataList")]
public class ItemDataList : ScriptableObject
{
    public List<ItemTypeData> itemDataListOfScriptableObject;
}
//
[System.Serializable]
public class ItemTypeData
{
    public ItemType itemType;
    public List<Items> itemsList;
}
