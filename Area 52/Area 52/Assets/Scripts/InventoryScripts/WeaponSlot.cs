using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This class is assigned to every weapon field and sets player items when dropped
public class WeaponSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Item.ItemType slotType;
    public Player player;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragDrop>().getItem().itemType == slotType)
        {
            Item currentItem = eventData.pointerDrag.GetComponent<DragDrop>().getItem();
            eventData.pointerDrag.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
            switch (currentItem.itemType)
            {
                case Item.ItemType.Weapon:
                    player.setWeapon(currentItem);
                    break;
                case Item.ItemType.Armor:
                    player.setArmor(currentItem);
                    break;
                case Item.ItemType.Helmet:
                    player.setHelmet(currentItem);
                    break;
                default:
                    break;
            }
            player.gameObject.GetComponent<InventoryObject>().inventoryClass.RemoveItem(currentItem);
            currentItem.parrentInventory = null;
        }

        
    }
}
