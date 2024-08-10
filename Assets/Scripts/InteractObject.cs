using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    private HasBeenInteractedHolder hasBeenInteractedHolder;
    private PreRequisite preRequisite;
    private ClickObjects clickObjects;
    
    [Header("TextStuffs:")] [Space(10)]
    public DialogueController dialogueController;
    public string[] Sentences;
    [Space(20)]

    [Header("Animations:")] [Space(10)]
    [SerializeField] private int failAnimationDelay;
    [SerializeField] private int animationDelay;
    [Space(13)]
    [SerializeField] private GameObject FailNotification;
    public GameObject VisualObject;
    
    [Space(20)]
    private bool CanProceed;



    private void Start()
    {
        hasBeenInteractedHolder = GetComponent<HasBeenInteractedHolder>();
        preRequisite = GetComponent<PreRequisite>();

        preRequisite.CheckConditions();             //Check if prerequisites are met
        CanProceed = preRequisite.conditionsMet;

        clickObjects = FindObjectOfType<ClickObjects>();
        FailNotification.SetActive(false);
        VisualObject.SetActive(false);
    }

    public void Click_Interact()
    {
        if (CanProceed)
        {
            StartCoroutine(startSystem());
        }

        else
        {
            StartCoroutine(cannotProceed());
        }
    }

    private void startDialogue()
    {
        clickObjects.CanClick = false;
        dialogueController.gameObject.SetActive(true);
        dialogueController.RecieveDialogue(Sentences);

        StartCoroutine(dialogueController.WriteSentence());
        hasBeenInteractedHolder.HasBeenInteracted = true;
    }

    private IEnumerator cannotProceed() //Animation for prerequisites not met
    {
        FailNotification.SetActive(true);
        yield return new WaitForSeconds(failAnimationDelay);
        FailNotification.SetActive(false);
    }

    private IEnumerator startSystem()
    {
        if(VisualObject != null)
        {
            VisualObject.SetActive(true);
            yield return new WaitForSeconds(animationDelay);
            startDialogue();
        }

        else
        {
            yield return new WaitForSeconds(0f);
            startDialogue();
        }
    }
}
