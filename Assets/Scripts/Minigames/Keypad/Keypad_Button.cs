using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keypad_Button : MonoBehaviour
{
    private Button button;
    public int number;
    public Keypad_Main keypadMain;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null && keypadMain != null)
        { 
            button.onClick.AddListener(PassNumber);
        }
    }

    public void PassNumber()
    {
        string numberToPass = number.ToString();
        keypadMain.AppendToString(numberToPass);
    }
}
