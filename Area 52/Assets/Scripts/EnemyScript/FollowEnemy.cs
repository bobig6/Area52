using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used so the canvas of the enemy with the health bar can follow them
public class FollowEnemy : MonoBehaviour
{
    // we get the object that we want to follow and the offset 
    public Transform objectToFollow;
    public Vector3 offset;
    void Update()
    {
        // and we update the position of the current object
        transform.position = objectToFollow.position + offset;
    }
}
