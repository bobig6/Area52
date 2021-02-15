using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script makes block slimy. That means that the player and enemy speeds are reduced by given divider when they step on it
public class SlimyBlock : StoppableObject
{
    public float divider = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (getEnabled())
        {
            //when the block is enabled we check if the collider is player or enemy
            if (collision.gameObject.name.Contains("Player"))
            {
                //if its player we reduce the movement speed by the divider
                collision.gameObject.GetComponent<PlayerMovement>().runSpeed /= divider;
            }
            else
            {
                collision.gameObject.transform.Find("EnemyObject").GetComponent<EnemyScript>().movingSpeed /= divider;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (getEnabled())
        {
            //then we return the speed to normal if the block is enabled
            if (collision.gameObject.name.Contains("Player"))
            {
                collision.gameObject.GetComponent<PlayerMovement>().runSpeed *= divider;
            }
            else
            {
                collision.gameObject.transform.Find("EnemyObject").GetComponent<EnemyScript>().movingSpeed *= divider;
            }
        }
    }
}
