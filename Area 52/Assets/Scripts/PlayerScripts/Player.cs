using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The main player class
public class Player : MonoBehaviour
{
    public GameObject weapons;
    public GameObject armors;
    public GameObject helmets;

    private Item helmet;
    private Item weapon;
    private Item armor;

    public Weapon currentWeapon;
    public Armor currentArmor;
    public Helmet currentHelmet;


    SaveClass saveClass = new SaveClass();

    // Start is called before the first frame update
    void Start()
    {
        Tooltip.HideTooltip_Static();
        helmet = null;
        armor = null;
        weapon = null;
        //Uncoment this and run the game once on build to clear the player data.
        //ClearPlayerData();
        LoadPlayer();
        ChangeWeapon();
        ChangeArmor();
        ChangeHelmet();
    }

    //Call this function when you want to save the game
    public void SavePlayer()
    {
        saveClass = new SaveClass();
        saveClass.profile.helmet = helmet;
        saveClass.profile.armor = armor;
        saveClass.profile.weapon = weapon;
        saveClass.profile.ProgressStageNumber = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex+1).name;
        saveClass.profile.health = GetComponent<PlayerHealth>().getHealth();
        saveClass.profile.playerInventory = GetComponent<InventoryObject>().inventoryClass;
        Debug.Log(saveClass.profile.ProgressStageNumber);
        SerializationManager.Save("PlayerData", saveClass);
    }

    //This function is called when we want to load the player 
    public void LoadPlayer()
    {
        saveClass = (SaveClass)SerializationManager.Load(Application.persistentDataPath + "/saves/PlayerData.save");
        if (saveClass != null)
        {
            helmet = saveClass.profile.helmet;
            armor = saveClass.profile.armor;
            weapon = saveClass.profile.weapon;
            if (SceneManager.GetActiveScene().name != saveClass.profile.ProgressStageNumber)
            {
                SceneManager.LoadScene(saveClass.profile.ProgressStageNumber);
            }
            GetComponent<InventoryObject>().inventoryClass = saveClass.profile.playerInventory;
            foreach (Item item in GetComponent<InventoryObject>().inventoryClass.inventory)
            {
                item.parrentInventory = gameObject.GetComponent<InventoryObject>();
            }
        }
    }

    public void ClearPlayerData()
    {
        saveClass = new SaveClass();
        saveClass.profile.ProgressStageNumber = SceneManager.GetActiveScene().name;
        saveClass.profile.playerInventory = GetComponent<InventoryObject>().inventoryClass;
        SerializationManager.Save("PlayerData", saveClass);
    }

    //function is called when the item in the weapon slot is changed
    public void ChangeWeapon()
    {
        //Setting default weapon if no other weapon is equipped
        if(weapon == null)
        {
            foreach (Transform child in weapons.transform)
            {
                child.gameObject.SetActive(false);
                if(child.GetComponent<Weapon>().thisWeapon.name == "Revolver")
                {
                    child.gameObject.SetActive(true);
                    currentWeapon = child.GetComponent<Weapon>();
                }
            }
            return;
        }
        //this goes through all weapon objects in the player and deactivates them
        foreach (Transform child in weapons.transform)
        {
            child.gameObject.SetActive(false);
            //if the object's name is the same as weapon name we activate it
            //that way we have all the weapons as deactivated objects and we activate only the one we need
            if(child.GetComponent<Weapon>().thisWeapon.name == weapon.name)
            {
                child.gameObject.SetActive(true);
                currentWeapon = child.GetComponent<Weapon>();
            }
        }

    }

    //function is called when the item in the armor slot is changed
    public void ChangeArmor()
    {
        // Deactivate all armor objects if no armor is equipped
        if (armor == null)
        {
            foreach (Transform child in armors.transform)
            {
                child.gameObject.SetActive(false);
            }
            return;
        }

        // if an item is equipped we find this item by name and we activate it and deactivate all others
        foreach (Transform child in armors.transform)
        {
            child.gameObject.SetActive(false);
            if (child.GetComponent<Armor>().thisArmor.name == armor.name)
            {
                child.gameObject.SetActive(true);
                currentArmor = child.GetComponent<Armor>();
            }
        }
    }

    //function is called when the item in the helmet slot is changed
    public void ChangeHelmet()
    {
        // Deactivate all helmet objects if no helmet is equipped
        if (helmet == null)
        {
            foreach (Transform child in helmets.transform)
            {
                child.gameObject.SetActive(false);
            }
            return;
        }

        // if an item is equipped we find this item by name and we activate it and deactivate all others
        foreach (Transform child in helmets.transform)
        {
            child.gameObject.SetActive(false);
            if (child.GetComponent<Helmet>().thisHelmet.name == helmet.name)
            {
                child.gameObject.SetActive(true);
                currentHelmet = child.GetComponent<Helmet>();
            }
        }
    }

    //When the player collides with something(gets shot)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            GetComponent<PlayerHealth>().reduceHealthPlayer(collision.gameObject.GetComponent<BulletScript>().damage);
        }
    }

    public Item getHelmet() { return helmet; }
    public Item getArmor() { return armor; }
    public Item getWeapon() { return weapon; }


    public void setHelmet(Item newItem) {
        // this function sets the item that is currently equipped
        // the difference between currentHelmet and helmet is that the currentHelmet is of type helmet and we
        // get the info about it when we activate the right helmet in ChangeHelmet()
        helmet = newItem;
        //when we unequip item we pass null as parameter and so we have to put the current helmet to be null as well
        if(helmet == null)
        {
            currentHelmet = null;
        }
        ChangeHelmet();
    }
    public void setWeapon(Item newItem) {
        weapon = newItem;
        if (weapon == null)
        {
            currentWeapon = null;
        }
        ChangeWeapon();
    }
    public void setArmor(Item newItem) {
        armor = newItem;
        if (armor == null)
        {
            currentArmor = null;
        }
        ChangeArmor();
    }

}
