using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * This script is responsible for opening and closing doors
 * We have to give the closed and the opened sprite
 */
public class DoorScript : StoppableObject
{
    public Sprite closed;
    public Sprite opened;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the door is opened and if it is we disable the collider and change the sprite
        if(!getEnabled())
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = opened;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = closed;
        }
    }



}
