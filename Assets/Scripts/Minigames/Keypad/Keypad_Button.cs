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
    [SerializeField] private AudioClip soundEffect;
    private AudioSource audioSource;


    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null && keypadMain != null)
        { 
            button.onClick.AddListener(PassNumber);
        }

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

    public void PassNumber()
    {
        audioSource.PlayOneShot(soundEffect);
        string numberToPass = number.ToString();
        keypadMain.AppendToString(numberToPass);
    }
}
