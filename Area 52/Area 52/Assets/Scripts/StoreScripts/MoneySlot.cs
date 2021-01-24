using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This class holds the money in the store. It should be assigned to the MoneySlot object.
public class MoneySlot : MonoBehaviour, IDropHandler
{
    public int amount;
    private void Update()
    {
        //resseting the amount when there is no item in the slot
        if(transform.childCount == 0)
        {
            amount = 0;
        }
    }
    //When a money object is dropped I change the amount
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<DragDrop>().getItem().itemType == Item.ItemType.Coin)
            {
                Item currentItem = eventData.pointerDrag.GetComponent<DragDrop>().getItem();
                //if there are money in the slot I just add the amount and destroy the object
                if(transform.childCount > 0)
                {
                    //get the first child and change the amount
                    transform.GetChild(0).GetComponent<DragDrop>().getItem().amount += currentItem.amount;
                    amount = transform.GetChild(0).GetComponent<DragDrop>().getItem().amount;
                    //removing the item from the parrent inventory and destroing the object
                    if (currentItem.parrentInventory != null)
                    {
                        currentItem.parrentInventory.inventoryClass.RemoveItem(currentItem);
                    }
                    Destroy(eventData.pointerDrag);
                }
                else
                {
                    //if there are no children in the inventory then I set this item to be a child
                    //and set the amount
                    eventData.pointerDrag.transform.SetParent(transform);
                    //removing item from its parrent inventory
                    if (currentItem.parrentInventory != null)
                    {
                        currentItem.parrentInventory.inventoryClass.RemoveItem(currentItem);
                    }
                    //setting parrent inventory to null because its just a slot and it doesn't have an inventory
                    currentItem.parrentInventory = null;
                    //setting the position of the item to be in the center
                    eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
                    //setting the current ammount
                    amount = currentItem.amount;
                }
            }
        }
    }
}
