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
    public MiniGameBool miniGameBool;
    private bool Completed;

    public float CompletionDelay = 5f;
    public float disableDelay = 5f;

    private Animator MainAnimator;
    private ClickObjects clickObjects;


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
        if (currentString == CorrectAnswer && !Completed)
        {
            //Debug.Log("Correct Answer!");
            Completed = true;
            miniGameBool.isCompleted = true;

            StartCoroutine(completion());
        }
        else
        {
            Completed = false;
            //Debug.Log("Incorrect Answer.");
        }
    }

    public void ClearString()
    {
        currentString = "";
        UpdateDisplay();
    }

    private IEnumerator completion()
    {
        miniGameBool.isCompleted = true;
        MainAnimator.SetTrigger("Keypad_Disable");
        yield return new WaitForSeconds(CompletionDelay);
        this.gameObject.SetActive(false);
        clickObjects.CanClick = true;
    }

    public void UIDisable()
    {
        if (!Completed)
        {
            StartCoroutine(disableUI());
        }
    }

    private IEnumerator disableUI()
    {
        MainAnimator.SetTrigger("Padlock_Disable");
        yield return new WaitForSeconds(disableDelay);
        this.gameObject.SetActive(false);
    }
}
