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

    [SerializeField] private AudioClip soundEffect;
    private AudioSource audioSource;

    private void Start()
    {
        UpdateText();

        GameObject sfxSourceObject = GameObject.Find("SFXSource");

        if (sfxSourceObject != null)
        {
            audioSource = sfxSourceObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("SFXSource GameObject not found in the scene.");
        }
    }

    public void Increase()
    {
        audioSource.PlayOneShot(soundEffect);
        currentAnswer = (currentAnswer + 1) % 10;
        UpdateText();
        CheckAnswer();
    }

    public void Decrease()
    {
        audioSource.PlayOneShot(soundEffect);
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

