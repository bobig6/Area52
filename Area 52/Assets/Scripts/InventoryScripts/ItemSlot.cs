using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//This class is assigned to every item slot in UI Inventory, UI chest and ect. It is used to reassign the item and move it when it is dropped
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public UI_Inventory ui_Inventory;
    public Player player;

    //The logic is that I keep the current inventory that this slot belongs to. And in OnDrop() I put the item in this slot
    //And I remove the item from the Inventory it belongs to. And I update the current inventory that the item belongs to.
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            Item current_item = eventData.pointerDrag.GetComponent<DragDrop>().getItem();
            eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
            ui_Inventory.inventoryObject.inventoryClass.inventory.Add(current_item);
            if(current_item.parrentInventory != null)
            {
                current_item.parrentInventory.inventoryClass.inventory.Remove(current_item);
            }
            else
            {
                if(player.getWeapon() == current_item)
                {
                    player.setWeapon(null);
                }
                else if (player.getArmor() == current_item)
                {
                    player.setArmor(null);
                }
                else if (player.getHelmet() == current_item)
                {
                    player.setHelmet(null);
                }
            }
            current_item.parrentInventory = ui_Inventory.inventoryObject;
            eventData.pointerDrag.transform.SetParent(ui_Inventory.getContainer());
        }
    }
}
