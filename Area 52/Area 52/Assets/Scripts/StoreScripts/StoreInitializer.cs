using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
/*
This class initializes the store buttons. It shoud be assigned to the ButtonListContainer gameObject and
the following parameters should be set:
> Items - list of items in the inventory. You should set their parameters and they will be initialized as buttons
> Money Slot - the slot where you put your money. It should have the MoneySlot class.
> Item Slot - the slot where you want your buyed item to apear.
> itemSlotTemplate - the tamplate for item creation.
> spriteHolder - the sprite holder in the scene that is used to find a sprite by a name.
*/
public class StoreInitializer : MonoBehaviour
{
	[SerializeField] List<Item> items;
	GameObject buttonPrefab;
	public List<Transform> buttonList;
	[SerializeField] Transform MoneySlot;
	[SerializeField] Transform ItemSlot;
	[SerializeField] Transform itemSlotTemplate;
	[SerializeField] SpriteHolder spriteHolder;

	private void Start()
	{
		//setting the template for the buttons
		buttonPrefab = transform.Find("StoreButtonPrefab").gameObject;
		//for each item in the list I create a button
		foreach(Item item in items){
			Transform button = Instantiate(buttonPrefab, transform).transform;
			//setting the parameters of the button
			button.GetChild(0).GetComponent<Text>().text = item.name;
			button.GetChild(1).GetComponent<Text>().text = item.buyPrice.ToString() + "$";
			button.GetChild(2).GetComponent<Image>().sprite = spriteHolder.getSprite(item.name);
			button.name = item.name;
			//Adding the function that is called when the button is clicked : BuyItem with index of the item in the list
			button.GetComponent<Button>().onClick.AddListener(delegate () { BuyItem(items.IndexOf(item)); });
			button.gameObject.SetActive(true);
			//Adding the button to a list to use it later
			buttonList.Add(button);
		}
	}
	//Setting all the buttons clickability based on the amount of money in the slot
	private void Update()
	{
		for (int i = 0; i < items.Count; i++)
		{
			if(MoneySlot.GetComponent<MoneySlot>().amount < items[i].buyPrice)
			{
				buttonList[i].GetComponent<Button>().interactable = false;
			}
			else
			{
				buttonList[i].GetComponent<Button>().interactable = true;
			}
		}
	}

	//BuyItem func is called when you click the buy button. It gets the item with the given index and creates icon for it
	public void BuyItem(int index)
	{
		if (ItemSlot.childCount == 0)
		{
			RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, ItemSlot).GetComponent<RectTransform>();
			itemSlotRectTransform.gameObject.SetActive(true);

			Item item = items[index];
			//Here I need to create a new item because if I dont when I try to add it to the player inventory list
			//It theates them as the same item and if I try to have more than one only one will be added
			Item storeItem = new Item();
			storeItem.amount = item.amount;
			storeItem.name = item.name;
			storeItem.sellPrice = item.sellPrice;
			storeItem.buyPrice = item.buyPrice;
			storeItem.isStackable = item.isStackable;
			storeItem.itemType = item.itemType;
			storeItem.parrentInventory = null;

			//setting sprite of item
			Image image = itemSlotRectTransform.Find("ItemImage").GetComponent<Image>();
			image.sprite = spriteHolder.getSprite(storeItem.name);
			itemSlotRectTransform.GetComponent<DragDrop>().setItem(storeItem);
			//set amount text
			Text amountText = itemSlotRectTransform.Find("AmountText").GetComponent<Text>();
			if (storeItem.amount > 1)
			{
				amountText.text = storeItem.amount.ToString();
			}
			else
			{
				amountText.text = "";
			}

			itemSlotRectTransform.position = ItemSlot.position;

			//remove the money from the slot
			if(storeItem.amount == MoneySlot.GetComponent<MoneySlot>().amount)
			{
				Destroy(MoneySlot.GetChild(0));
				MoneySlot.GetComponent<MoneySlot>().amount = 0;
			}
			else
			{
				MoneySlot.GetChild(0).GetComponent<DragDrop>().removeAmountToItem(storeItem.buyPrice);				
				Text moneyAmountText = MoneySlot.GetChild(0).Find("AmountText").GetComponent<Text>();
				if (MoneySlot.GetChild(0).GetComponent<DragDrop>().getItem().amount > 1)
				{
					moneyAmountText.text = MoneySlot.GetChild(0).GetComponent<DragDrop>().getItem().amount.ToString();
				}
				else
				{
					moneyAmountText.text = "";
				}
				MoneySlot.GetComponent<MoneySlot>().amount = MoneySlot.GetChild(0).GetComponent<DragDrop>().getItem().amount;
			}
		}
	}



}
