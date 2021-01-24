using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is responsible for managing UI and player input for inventory, stores and puzzles
 * The needed variables are the ui elements of every object.
 */
public class PlayerInput : MonoBehaviour
{
    // UI Elements
    public UI_Inventory uI_Inventory;
    [SerializeField] UI_Inventory uI_Chest;
    [SerializeField] GameObject equippedItems;
    [SerializeField] GameObject trashKings;
    [SerializeField] GameObject alienStore;
    [SerializeField] GameObject codePuzzle;
    [SerializeField] GameObject hackingPuzzle;
    [SerializeField] GameObject buttonPuzzle;
    
    // Boolean for activity of the inventory
    public static bool isInventoryActive = false;
    
    // Booleans for states of UI elements
    InventoryObject ChestObject;
    bool isTrashKingsTriggered = false;
    bool isAlienStoreTriggered = false;
    bool isCodePuzzleTriggered = false;
    bool isHackPanelPuzzleTriggered = false;
    bool isButtonPuzzleTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        //Setting all UI objects to unactive
        uI_Inventory.gameObject.SetActive(false);
        uI_Chest.gameObject.SetActive(false);
        equippedItems.SetActive(false);
        trashKings.SetActive(false);
        alienStore.SetActive(false);
        codePuzzle.SetActive(false);
        hackingPuzzle.SetActive(false);
        buttonPuzzle.SetActive(false);
        ChestObject = null;
    }

    // Update is called once per frame

    void Update()
    {
        // if we press the "E" button if the inventory isn't active it activates needed elements
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInventoryActive)
            {
                isInventoryActive = true;
                uI_Inventory.RefreshInventoryItems();
                uI_Inventory.gameObject.SetActive(true);
                //open chest
                if (ChestObject)
                {
                    uI_Chest.RefreshInventoryItems();
                    uI_Chest.gameObject.SetActive(true);
                    //show elements
                }
                //open trash kings shop
                else if (isTrashKingsTriggered)
                {
                    trashKings.SetActive(true);
                }
                //open alien store
                else if (isAlienStoreTriggered)
                {
                    alienStore.SetActive(true);
                }
                //open code puzzle
                else if (isCodePuzzleTriggered)
                {
                    uI_Inventory.gameObject.SetActive(false);
                    codePuzzle.SetActive(true);
                }
                //open hacking Puzzle
                else if (isHackPanelPuzzleTriggered)
                {
                    uI_Inventory.gameObject.SetActive(false);
                    hackingPuzzle.SetActive(true);
                }
                //open button Puzzle
                else if (isButtonPuzzleTriggered)
                {
                    uI_Inventory.gameObject.SetActive(false);
                    buttonPuzzle.SetActive(true);
                }
                //open equipped items 
                else
                {
                    equippedItems.GetComponent<EquippedItems>().RefreshItems();
                    equippedItems.gameObject.SetActive(true);
                }
            }
            //if the inventory is already active it deactivates everything
            else
            {
                isInventoryActive = false;
                uI_Inventory.gameObject.SetActive(false);
                uI_Chest.gameObject.SetActive(false);
                equippedItems.gameObject.SetActive(false);
                Tooltip.HideTooltip_Static();
                trashKings.SetActive(false);
                alienStore.SetActive(false);
                codePuzzle.SetActive(false);
                hackingPuzzle.SetActive(false);
                buttonPuzzle.SetActive(false);
            }
        }
    }


    // When player comes in contact with trigger it checks the name and changes the boolean of the 
    // object we triggered with to true
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Chest"))
        {
            ChestObject = collision.gameObject.GetComponent<InventoryObject>();
            uI_Chest.inventoryObject = ChestObject;
        }
        else if (collision.gameObject.name.Contains("TrashKings"))
        {
            isTrashKingsTriggered = true;
        }
        else if (collision.gameObject.name.Contains("AlienStore"))
        {
            isAlienStoreTriggered = true;
        }
        else if (collision.gameObject.name.Contains("CodePuzzle"))
        {
            isCodePuzzleTriggered = true;
        }
        else if (collision.gameObject.name.Contains("HackingPanelPuzzle"))
        {
            isHackPanelPuzzleTriggered = true;
        }
        else if (collision.gameObject.name.Contains("ButtonPuzzle"))
        {
            isButtonPuzzleTriggered = true;
        }
    }
    // When the player isn't triggered with the object anymore we set it's boolean value to false
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Chest"))
        {
            ChestObject = null;
        }
        else if (collision.gameObject.name.Contains("TrashKings"))
        {
            isTrashKingsTriggered = false;
        }
        else if (collision.gameObject.name.Contains("AlienStore"))
        {
            isAlienStoreTriggered = false;
        }
        else if (collision.gameObject.name.Contains("CodePuzzle"))
        {
            isCodePuzzleTriggered = false;
        }
        else if (collision.gameObject.name.Contains("HackingPanelPuzzle"))
        {
            isHackPanelPuzzleTriggered = false;
        }
        else if (collision.gameObject.name.Contains("ButtonPuzzle"))
        {
            isButtonPuzzleTriggered = false;
        }
    }

}
