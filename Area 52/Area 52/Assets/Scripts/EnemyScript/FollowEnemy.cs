﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;
    void Update()
    {
        transform.position = objectToFollow.position + offset;
    }
}
