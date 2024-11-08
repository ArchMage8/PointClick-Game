using System.Collections;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    private ClickObjects clickObjects;
    private bool tutActive;

    [Header("Text Settings")]
    public DialogueController dialogueController;
    public string[] sentences;

    [Header("Effects")]
    public GameObject textFade;
    public int startDelay;

    private void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
        clickObjects.CanClick = false;
        dialogueController.ReceiveDialogue(sentences, null);
        StartCoroutine(StartTutorial());
    }

    private void Update()
    {
        if (tutActive && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            if (dialogueController.Index >= sentences.Length)
            {
                EndTutorial();
            }
            else
            {
                dialogueController.NextSentence();
            }
        }
    }

    private IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(startDelay);
        StartDialogue();
    }

    private void StartDialogue()
    {
        textFade.SetActive(true);
        tutActive = true;
        StartCoroutine(dialogueController.WriteSentence());
    }

    private void EndTutorial()
    {
        textFade.SetActive(false);
        tutActive = false;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        clickObjects.CanClick = true;
    }
}
