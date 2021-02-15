using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class holds info for every item and is used in the Inventory class;
[Serializable]
public class Item
{
    //A class for types of items and their amount;
    public enum ItemType
    {
        Weapon,
        Armor,
        Helmet,
        Coin,
    }
    private System.Random _random = new System.Random();
    private readonly float itemId;

    public InventoryObject parrentInventory;
    public string name;
    public ItemType itemType;
    public int amount;

    public int buyPrice;
    public int sellPrice;

    public bool isStackable;

    public Item()
    {
        itemId = _random.Next();
    }

}
