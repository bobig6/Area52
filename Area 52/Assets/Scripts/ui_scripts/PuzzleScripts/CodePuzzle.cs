using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * This script is putted onto the ui code puzzle
 * This puzzle generates random number that should be entered in a box
 * When the puzzle is solved isOpened variable becomes True
 */
public class CodePuzzle : MonoBehaviour
{
    private Text codeText;
    private bool isOpened = false;
    public StoppableObject objectToStop;

    int code;

    // Start is called before the first frame update
    void Start()
    {
        //finding the text object in the children
        codeText = transform.Find("Text").GetComponent<Text>();
        //generating random number
        System.Random r = new System.Random();
        code = r.Next(0, 9999999);
        //setting the number to the text object
        codeText.text = code.ToString();
    }

    //This function is assigned to the OnValueChanged of the input field
    public void checkCode(string input)
    {
        int inputCode = 0;
        //if the right value is entered we change the bool to true and close the window
        if(int.TryParse(input, out inputCode))
        {
            if(inputCode == code)
            {
                isOpened = true;
                if (objectToStop != null) objectToStop.changeState(!isOpened);
                gameObject.SetActive(false);
                PlayerInput.isInventoryActive = false;
            }
        }
    }
}
