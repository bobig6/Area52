using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//This class is assigned to player and chest UIs and refreshes them.
public class UI_Inventory : MonoBehaviour
{
    public InventoryObject inventoryObject;
    private Transform background_image;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    public GameObject itemSlotHolder;

    public List<RectTransform> ui_inventory;
    public SpriteHolder spriteHolder;

    private void Awake()
    {
        background_image = transform.Find("Background");
        itemSlotContainer = background_image.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        RefreshInventoryItems();
    }

    public Transform getContainer()
    {
        return itemSlotContainer;
    }


    public void RefreshInventoryItems()
    {   
        
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 125f;
        foreach(Item item in inventoryObject.inventoryClass.inventory)
        {
            RectTransform itemSlotRectTransform =  Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            ui_inventory.Add(itemSlotRectTransform);
            itemSlotRectTransform.gameObject.SetActive(true);
            //setting sprite of item
            Image image = itemSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = spriteHolder.getSprite(item.name);
            itemSlotRectTransform.GetComponent<DragDrop>().setItem(item);
            //set amount text
            Text amountText = itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
            if(item.amount > 1)
            {
                amountText.text = item.amount.ToString();
            }
            else
            {
                amountText.text = "";
            }

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x++;
            if(x > 2)
            {
                x = 0;
                y--;
            }
            
        }
    }
    
}
