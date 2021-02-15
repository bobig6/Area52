using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is parent of every object that can be enabled and disabled aka tuuret, doors, damage blocks etc.
public class StoppableObject : MonoBehaviour
{
    // we have a boolean for whether the object is enabled
    private bool isEnabled = true;

    // this function returns the state of the object(enabled or disabled)
    public bool getEnabled()
    {
        return isEnabled;
    }

    // this func changes the state of the object to the given state
    public void changeState(bool state)
    {
        isEnabled = state;
    }
}
