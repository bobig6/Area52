using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellItemFrame : MonoBehaviour, IDropHandler
{
    TrashKingsScript trashKings;

    private void Awake()
    {
        trashKings = transform.parent.GetComponent<TrashKingsScript>();
    }

    //The logic is that I keep the current inventory that this slot belongs to. And in OnDrop() I put the item in this slot
    //And I remove the item from the Inventory it belongs to. And I update the current inventory that the item belongs to.
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Item current_item = eventData.pointerDrag.GetComponent<DragDrop>().getItem();
            eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
            if (current_item.parrentInventory != null)
            {
                current_item.parrentInventory.inventoryClass.inventory.Remove(current_item);
            }
            trashKings.generateMoney(current_item.amount * current_item.sellPrice);
            Destroy(eventData.pointerDrag);
        }
    }
}
