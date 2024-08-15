using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad_Main : MonoBehaviour
{
    public TMP_Text displayText;
    public string CorrectAnswer = "1234";
    public string currentString = "";
    private int MaxLength = 4;

    public void AppendToString(string value)
    {
        if (currentString.Length + value.Length <= MaxLength)
        {
            currentString += value;
        }
        else
        {
            currentString = (currentString + value).Substring(0, MaxLength);
        }
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = currentString;
        }
    }

    public void CheckAnswer()
    {
        if (currentString == CorrectAnswer)
        {
            Debug.Log("Correct Answer!");
        }
        else
        {
            Debug.Log("Incorrect Answer.");
        }
    }

    public void ClearString()
    {
        currentString = "";
        UpdateDisplay();
    }
}
