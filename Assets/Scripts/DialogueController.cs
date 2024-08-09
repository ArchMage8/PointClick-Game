using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
  
    [HideInInspector]public string[] Sentences;
    [HideInInspector]public int Index = 0;
    private bool isTyping = false;
    [SerializeField] private float writeSpeed;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DialogueText.isActiveAndEnabled)
        {
            NextSentence();
        }
        else
        {
            return;
        }
    }

    public void NextSentence()
    {
        if(Index <= Sentences.Length - 1 && !isTyping)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
    }


    IEnumerator WriteSentence()
    {
        foreach(char Character in Sentences[Index].ToCharArray())
        {
            DialogueText.text += Character;
            isTyping = true;
            yield return new WaitForSeconds(writeSpeed);
        }
        isTyping = false;
        Index++;
    }

    public void RecieveDialogue(string[] SentSentences)
    {
        Sentences = new string[SentSentences.Length];
        for(int i=0; i<SentSentences.Length; i++)
        {
            Sentences[i] = SentSentences[i];
        }
    }
}
