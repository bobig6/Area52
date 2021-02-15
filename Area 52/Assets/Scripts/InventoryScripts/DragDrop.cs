using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//This class is assigned to every item in the UI_Inventory and handles the drag and drop system
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup canvasGroup;
    private Item item;
    public UI_Inventory ui_Inventory;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public Item getItem()
    {
        return item;
    }

    public void addAmountToItem(int amount)
    {
        item.amount += amount;
    }
    
    public void removeAmountToItem(int amount)
    {
        item.amount -= amount;
    }

    public void setItem(Item item)
    {
        this.item = item;
    }

    public void OnDrag(PointerEventData eventData){}

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData){}


    //when item is dropped on another item if they are stackable the item is removed from the inventory it is in
    //And the ammount of the current item is increased
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<DragDrop>().getItem().itemType == this.item.itemType && this.item.isStackable)
        {
            Item currentItem = eventData.pointerDrag.GetComponent<DragDrop>().getItem();
            this.item.amount += currentItem.amount;
            if(currentItem.parrentInventory != null)
            {
                currentItem.parrentInventory.inventoryClass.RemoveItem(currentItem);
            }

            ui_Inventory.RefreshInventoryItems();
            Destroy(eventData.pointerDrag);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string description = "Name: " + item.name +
                             "\nType: " + item.itemType.ToString() +
                             "\nBuy price: " + item.buyPrice.ToString() +
                             "\nSell price: " + item.sellPrice.ToString();
        Tooltip.ShowTooltip_Static(description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();
    }
}
