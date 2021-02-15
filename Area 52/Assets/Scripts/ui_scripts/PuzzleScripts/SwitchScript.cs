using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is responsible for switching the buttons in the ButtonPuzzle
 * We have to put it on every switch button and assign the puzzle instance
 */
public class SwitchScript : MonoBehaviour
{
    public ButtonPuzzle puzzle;
    public bool isOn = false;
    void Start()
    {
        //Sets the default value to the switch
        transform.GetChild(0).gameObject.SetActive(isOn);
        transform.GetChild(1).gameObject.SetActive(isOn);
        if (isOn)
        {
            puzzle.SwitchChange(1);
        }
    }
    // This function is called when we press a button to change a switch
    public void OnMouseUp()
    {
        isOn = !isOn;
        //changing the value on the children that are the button image and the light image
        transform.GetChild(0).gameObject.SetActive(isOn);
        transform.GetChild(1).gameObject.SetActive(isOn);
        //if its on we add 1 to the OnCount, otherwise we add -1
        if (isOn)
        {
            puzzle.SwitchChange(1);
        }else
        {
            puzzle.SwitchChange(-1);
        }
    }
}
