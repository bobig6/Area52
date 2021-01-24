using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script allows us to create objects that can be picked up and moved by the player
    to make a block like that you will need a empty child GameObject that has a BoxCollider2D and it is set to Trigger
    and you need to assign this script to it and ajust the offset when picking
*/
public class PickableBlock : MonoBehaviour
{
    public Vector3 offset;
    private Transform picker = null;
    private bool isPicked = false;


    // Update is called once per frame
    void Update()
    {
        if (picker!= null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isPicked = !isPicked;
                transform.parent.GetComponent<Rigidbody2D>().isKinematic = isPicked;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPicked)
        {
            transform.parent.position = picker.position + offset;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            picker = collision.gameObject.transform;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isPicked)
        {
            picker = null;
        }

    }
}
