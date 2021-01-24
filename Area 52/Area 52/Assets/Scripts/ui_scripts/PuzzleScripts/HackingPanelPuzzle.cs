using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HackingPanelPuzzle : MonoBehaviour
{
    TMP_InputField inputField;

    Text consoleText;
    Text accessText;

    bool isFirstCompleted = false;
    bool isSecondCompleted = false;
    void Start()
    {
        inputField = transform.Find("InputField").GetComponent<TMP_InputField>();
        consoleText = transform.Find("ConsoleText").GetComponent<Text>();
        accessText = transform.Find("AccessText").GetComponent<Text>();
    }

    public void checkInput(string input)
    {
        if (!isFirstCompleted)
        {
            if(input == "gcc hack_program.c")
            {
                consoleText.text += input;
                consoleText.text += "\nC:\\Users\\trash_king69> ";
                isFirstCompleted = true;
                inputField.text = "";
            }
        }
        else
        {
            if (input == "./a.out")
            {
                consoleText.text += input;
                isSecondCompleted = true;
                inputField.text = "";
                accessText.color = Color.green;
                accessText.text = "Access Granted";
                gameObject.SetActive(false);
                PlayerInput.isInventoryActive = false;
            }
        }
    }
}
