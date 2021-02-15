using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashKingsScript : MonoBehaviour
{
    private Transform moneyObject;
    private Transform itemSlotTemplate;
    public SpriteHolder spriteHolder;

    private void Awake()
    {
        moneyObject = transform.Find("Money").GetComponent<Transform>();
        itemSlotTemplate = transform.Find("ItemSlotTemplate");
    }

    public void generateMoney(int amount)
    {
        if(moneyObject.childCount == 0)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, moneyObject).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            //creating the new item
            Item item = new Item();
            item.amount = amount;
            item.name = "Money";
            item.sellPrice = 1;
            item.buyPrice = 1;
            item.isStackable = true;
            item.itemType = Item.ItemType.Coin;
            item.parrentInventory = null;

            //setting sprite of item
            Image image = itemSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = spriteHolder.getSprite("Money");
            itemSlotRectTransform.GetComponent<DragDrop>().setItem(item);
            //set amount text
            Text amountText = itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
            if (item.amount > 1)
            {
                amountText.text = item.amount.ToString();
            }
            else
            {
                amountText.text = "";
            }

            itemSlotRectTransform.position = moneyObject.position;
        }
        else
        {
            RectTransform itemSlotRectTransform = moneyObject.GetChild(0).GetComponent<RectTransform>();
            itemSlotRectTransform.GetComponent<DragDrop>().addAmountToItem(amount);
            //set new amount text
            Text amountText = itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
            amountText.text = itemSlotRectTransform.GetComponent<DragDrop>().getItem().amount.ToString();
           

        }
        
    }
}
