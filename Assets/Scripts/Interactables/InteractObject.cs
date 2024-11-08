using System.Collections;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    private HasBeenInteractedHolder hasBeenInteractedHolder;
    private PreRequisite preRequisite;
    private ClickObjects clickObjects;

    [Header("Text Settings")]
    public DialogueController dialogueController;
    public string[] sentences;
    public string[] colorHexCodes;

    [Header("Animations")]
    [SerializeField] private int failAnimationDelay;
    [SerializeField] private int animationDelay;
    [SerializeField] private GameObject failNotification;
    public GameObject visualObject;
    public GameObject textFade;

    [SerializeField] private AudioClip soundEffect;
    private AudioSource audioSource;
    private bool canProceed;

    private void Start()
    {
        hasBeenInteractedHolder = GetComponent<HasBeenInteractedHolder>();
        preRequisite = GetComponent<PreRequisite>();

        preRequisite.CheckConditions();
        canProceed = preRequisite.conditionsMet;

        clickObjects = FindObjectOfType<ClickObjects>();
        failNotification.SetActive(false);
        visualObject.SetActive(false);
        textFade.SetActive(false);

        audioSource = GameObject.Find("SFXSource")?.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("SFXSource GameObject not found in the scene.");
        }
    }

    private void Update()
    {
        if (visualObject.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            if (dialogueController.Index >= sentences.Length)
            {
                EndDialogue();
            }
            else
            {
                dialogueController.NextSentence();
                if (sentences.Length != colorHexCodes.Length)
                {
                    Debug.LogError("Length of color and text arrays need to be the same");
                }
            }
        }
    }

    public void Click_Interact()
    {
        canProceed = preRequisite.conditionsMet;
        if (canProceed)
        {
            audioSource.PlayOneShot(soundEffect);
            dialogueController.ReceiveDialogue(sentences, colorHexCodes);
            StartCoroutine(StartDialogueSystem());
        }
        else
        {
            StartCoroutine(CannotProceed());
        }
    }

    private void StartDialogue()
    {
        textFade.SetActive(true);
        clickObjects.CanClick = false;
        StartCoroutine(dialogueController.WriteSentence());
        hasBeenInteractedHolder.HasBeenInteracted = true;
    }

    private IEnumerator CannotProceed()
    {
        if (failNotification != null)
        {
            failNotification.SetActive(true);
            textFade.SetActive(true);
            yield return new WaitForSeconds(failAnimationDelay);
            failNotification.SetActive(false);
            textFade.SetActive(false);
        }
    }

    private IEnumerator StartDialogueSystem()
    {
        if (visualObject != null && dialogueController.Index == 0)
        {
            visualObject.SetActive(true);
            dialogueController.gameObject.SetActive(true);
            yield return new WaitForSeconds(animationDelay);
            StartDialogue();
        }
        else if (dialogueController.Index == 0)
        {
            StartDialogue();
        }
    }

    private void EndDialogue()
    {
        visualObject.SetActive(false);
        textFade.SetActive(false);
        clickObjects.CanClick = true;
        
    }
}
