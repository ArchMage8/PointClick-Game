using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    private TextMeshProUGUI DialogueText;

    private ClickObjects clickObjects;
    [HideInInspector]public string[] Sentences;
    [HideInInspector]public int Index = 0;
    private bool isTyping = false;
    [SerializeField] private float writeSpeed;

    private void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
        Transform DialogueTextObject = transform.GetChild(0);
        DialogueText = DialogueTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        
        if (DialogueText.isActiveAndEnabled)
        {

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {

            if (Index >= Sentences.Length)
            {
                StartCoroutine(DisableThis()); // Call your coroutine here
            }

            else 
            { 
            NextSentence();
            }

        }
        else
        {
            return;
        }

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


   public IEnumerator WriteSentence()
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

    IEnumerator DisableThis()
    {
        yield return new WaitForSeconds(0f);

        DialogueText.text = "";
        clickObjects.CanClick = true;
        Index = 0;
        Sentences = new string[0];
        this.gameObject.SetActive(false);
    }
}
