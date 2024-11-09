using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    private ClickObjects clickObjects;
    private bool TutActive;

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
        clickObjects.CanClick = false;
        dialogueController.RecieveDialogue(Sentences);
        StartCoroutine(Starting());
    }

    private void Update()
    {
        if (TutActive)
        {
            if (dialogueController.Index >= Sentences.Length)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    TextFade.SetActive(false);
                    TutActive = false;
                    StartCoroutine(StartGame());
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                dialogueController.NextSentence();
            }
        }
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(startDelay);
        TutDialogue();
    }

    private void TutDialogue()
    {
        TextFade.SetActive(true);
        TutActive = true;
        StartCoroutine(startSystem());
    }

    private IEnumerator startSystem()
    {
        if (dialogueController.Index == 0)
        {
            dialogueController.gameObject.SetActive(true);

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

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        clickObjects.CanClick = true;
    }
}
