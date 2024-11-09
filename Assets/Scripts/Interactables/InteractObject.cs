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
    public string[] colorHexCodes;

    [Space(20)]

    [Header("Animations:")] [Space(10)]
    [SerializeField] private int failAnimationDelay;
    [SerializeField] private int animationDelay;
    [Space(13)]
    [SerializeField] private GameObject FailNotification;
    public GameObject VisualObject;
    public GameObject TextFade;
    
    [Space(20)]
    [SerializeField] private AudioClip soundEffect;
    private AudioSource audioSource;
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
        TextFade.SetActive(false);

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

    private void Update()
    {
        if (VisualObject.activeSelf)
        {
            if (dialogueController.Index >= Sentences.Length)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    VisualObject.SetActive(false);
                    TextFade.SetActive(false);
                }
            }

            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                dialogueController.NextSentence();

                if(Sentences.Length != colorHexCodes.Length)
                {
                    Debug.LogError("Length of color and text arrays need to be same");
                }
            }
        }
    }

    public void Click_Interact()
    {
        CanProceed = preRequisite.conditionsMet;
        if (CanProceed)
        {
            audioSource.PlayOneShot(soundEffect);
            dialogueController.RecieveDialogue(Sentences);
            dialogueController.RecieveColors(colorHexCodes);
            StartCoroutine(startSystem());
        }

        else if(!CanProceed)
        {
            Debug.Log("Cannot Proceed");
            StartCoroutine(cannotProceed());
        }
    }

    private void startDialogue()
    {
        Debug.Log("Start");

        TextFade.SetActive(true);
        clickObjects.CanClick = false;
     
        dialogueController.NextSentence();
        hasBeenInteractedHolder.HasBeenInteracted = true;
    }

    private IEnumerator cannotProceed() //Animation for prerequisites not met
    {

        if (FailNotification != null)
        {
            FailNotification.SetActive(true);
            TextFade.SetActive(true);
            yield return new WaitForSeconds(failAnimationDelay);
            clickObjects.CanClick = false;
            FailNotification.SetActive(false);
            TextFade.SetActive(false);
            clickObjects.CanClick = true;
        }
        else
        {
            yield return null;
        }
    }

    private IEnumerator startSystem()
    {
        
        if (VisualObject != null && dialogueController.Index == 0)
        {
            VisualObject.SetActive(true);
            dialogueController.gameObject.SetActive(true);
            yield return new WaitForSeconds(animationDelay);

            if (dialogueController.Index == 0)
            {
                startDialogue();
            }
        }

        else if(dialogueController.Index == 0)
        {
            yield return new WaitForSeconds(0f);
            startDialogue();
        }
    }

    
}
