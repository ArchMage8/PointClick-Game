using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    private ClickObjects clickObjects;

    [Header("TextStuffs:")]
    [Space(10)]
    public DialogueController dialogueController;
    public string[] Sentences;
    
    [Space(20)]

    [Header("Effects")]
    public GameObject TextFade;
    public int startDelay;

    private void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
        StartCoroutine(Starting());
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(startDelay);
        TutDialogue();
    }

    private void TutDialogue()
    {
        TextFade.SetActive(true);
        StartCoroutine(startSystem());
    }

    private IEnumerator startSystem()
    {

        if (dialogueController.Index == 0)
        {
           
            dialogueController.gameObject.SetActive(true);
            //yield return new WaitForSeconds(animationDelay);

            if (dialogueController.Index == 0)
            {
                startDialogue();
            }
        }

        else if (dialogueController.Index == 0)
        {
            yield return new WaitForSeconds(0f);
            startDialogue();
        }
    }

    private void startDialogue()
    {
        Debug.Log("Start");

        TextFade.SetActive(true);
        clickObjects.CanClick = false;

        StartCoroutine(dialogueController.WriteSentence());
    }
}
