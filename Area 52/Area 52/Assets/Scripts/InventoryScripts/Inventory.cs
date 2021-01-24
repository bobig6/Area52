using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is a data class and holds a list of items. Every player, chest and enemy has one.
[Serializable]
public class Inventory
{
    public List<Item> inventory = new List<Item>();




    public void AddItem(Item item)
    {
        //checks if item is stackable and if is 
        //searches if it already is in inventory    
        if (item.isStackable)
        {
            bool isAlreadyInInventory = false;
            foreach (Item inventoryItem in inventory)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    isAlreadyInInventory = true;
                }
            }
            if (!isAlreadyInInventory)
            {
                inventory.Add(item);
            }
        }
        else
        {
            inventory.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }

}
