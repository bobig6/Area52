using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is responsible for the doors that are unlocked
public class UnlockedDoor : MonoBehaviour
{ 
    private GameObject target = null;
    private bool isOpened = false;
    // We need sprites for when the door is closed and opened
    public Sprite closed;
    public Sprite opened;

    private void Update()
    {
        // if the target object exists (aka not null) and we press the F button
        if(target && Input.GetKeyDown(KeyCode.F))
        {
            // if the door is already opened 
            if (isOpened)
            {
                // we enable the collider, change the sprite and we change the value of the bool
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().sprite = closed;
                isOpened = false;
            }
            // if its closed we open it
            else
            {
                // the opposite of the one above
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = opened;
                isOpened = true;
            }
        }
    }

    // if the player enteres the trigger araa we set the player as target
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            target = collision.gameObject;
        }
    }

    // when the player exits the collider we set the target var to null
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            target = null;
        }
    }

}
