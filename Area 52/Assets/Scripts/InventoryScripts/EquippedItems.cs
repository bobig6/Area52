using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is assigned to the UI of the equiped items and it refreshes it.
public class EquippedItems : MonoBehaviour
{
    public GameObject helmetSlot;
    public GameObject armorSlot;
    public GameObject weaponSlot;

    public GameObject itemSlotTemplate;

    public Player player;

    public SpriteHolder spriteHolder;

    // Start is called before the first frame update
    void Start()
    {
        RefreshItems();
    }

    //Called every time before the equiped items are opened
    public void RefreshItems()
    {
        //Destroy previous objects
        foreach (Transform child in helmetSlot.transform)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (Transform child in armorSlot.transform)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (Transform child in weaponSlot.transform)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        //filling helmet slot
        if(player.getHelmet() != null)
        {
            RectTransform helmetSlotRectTransform = Instantiate(itemSlotTemplate, helmetSlot.transform.position, helmetSlot.transform.rotation).GetComponent<RectTransform>();
            helmetSlotRectTransform.gameObject.SetActive(true);
            Image image = helmetSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = spriteHolder.getSprite(player.getHelmet().name);
            helmetSlotRectTransform.GetComponent<DragDrop>().setItem(player.getHelmet());
            helmetSlotRectTransform.Find("AmountText").GetComponent<Text>().text = "";
            helmetSlotRectTransform.SetParent(helmetSlot.transform);
        }

        //filling armor slot
        if (player.getArmor() != null)
        {
            RectTransform armorSlotRectTransform = Instantiate(itemSlotTemplate, armorSlot.transform.position, armorSlot.transform.rotation).GetComponent<RectTransform>();
            armorSlotRectTransform.gameObject.SetActive(true);
            Image image = armorSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = spriteHolder.getSprite(player.getArmor().name);
            armorSlotRectTransform.GetComponent<DragDrop>().setItem(player.getArmor());
            armorSlotRectTransform.Find("AmountText").GetComponent<Text>().text = "";
            armorSlotRectTransform.SetParent(armorSlot.transform);
        }

        //filling weapon slot
        if (player.getWeapon() != null)
        {
            RectTransform weaponSlotRectTransform = Instantiate(itemSlotTemplate, weaponSlot.transform.position, weaponSlot.transform.rotation).GetComponent<RectTransform>();
            weaponSlotRectTransform.gameObject.SetActive(true);
            Image image = weaponSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = spriteHolder.getSprite(player.getWeapon().name);
            weaponSlotRectTransform.GetComponent<DragDrop>().setItem(player.getWeapon());
            weaponSlotRectTransform.Find("AmountText").GetComponent<Text>().text = "";
            weaponSlotRectTransform.SetParent(weaponSlot.transform);
        }

    }
}
