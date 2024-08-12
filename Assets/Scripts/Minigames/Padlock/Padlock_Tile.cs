using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Padlock_Tile : MonoBehaviour
{
    public bool isCorrect = false;
    [Space (20)]
    public int CorrectAnswer;
    public int currentAnswer = 0;
    [Space(20)]
    public TMP_Text answerText;

    private void Start()
    {
        UpdateText();
    }

    public void Increase()
    {
        currentAnswer = (currentAnswer + 1) % 10;
        UpdateText();
        CheckAnswer();
    }

    public void Decrease()
    {
        currentAnswer = (currentAnswer - 1 + 10) % 10;
        UpdateText();
        CheckAnswer();
    }

    public void UpdateText()
    {
        if (answerText != null)
        {
            answerText.text = currentAnswer.ToString();
        }
    }

    public void CheckAnswer()
    {
        isCorrect = (currentAnswer == CorrectAnswer);
    }
}

