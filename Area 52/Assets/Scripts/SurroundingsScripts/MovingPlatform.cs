using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script allows to make moving platforms. The following parameters are needed:
    - pos1 and pos2 are empty GameObjects that should be the starting and the ending points
    - speed is a float that controls the speed of the platform
    - isEnabled is a bool that can be used to stop the platform
*/
public class MovingPlatform : StoppableObject
{
    public Transform pos1, pos2;
    public float speed;


    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = pos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!getEnabled())
        {
            if (transform.position == pos1.position)
            {
                nextPos = pos2.position;
            }
            else if (transform.position == pos2.position)
            {
                nextPos = pos1.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }
    }

}
