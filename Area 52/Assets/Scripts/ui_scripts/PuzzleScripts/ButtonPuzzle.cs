using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script manages the ButtonPuzzle
 * We have boolean AreAllOn that indicates when the puzzle is completed
 */
public class ButtonPuzzle : MonoBehaviour
{
    private int OnCount = 0;
    public bool AreAllOn = false;
    public StoppableObject objectToStop;
    //This function adds points to the OnCount variable
    public void SwitchChange(int points)
    {
        OnCount = OnCount + points;
        //When the OnCount variable is 5 all the switches are on and the puzzle is completed
        if(OnCount == 5)
        {
            AreAllOn = true;
            if (objectToStop != null) objectToStop.changeState(!AreAllOn);
            gameObject.SetActive(false);
            PlayerInput.isInventoryActive = false;
        }
    }
}
