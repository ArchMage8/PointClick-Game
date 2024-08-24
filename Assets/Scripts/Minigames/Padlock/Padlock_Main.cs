using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Padlock_Main : MonoBehaviour
{
    public bool Completed = false;
    public Padlock_Tile[] padlockTiles;
    public GameObject First;
    public GameObject Second;
    public float CompletionDelay = 5f;
    public float disableDelay = 5f;
    [SerializeField] private AudioClip soundEffect;

    public GameObject TriggerObject;

    private Animator MainAnimator;
    private MiniGameBool miniGameBool;
    private ClickObjects clickObjects;
    private HasBeenInteractedHolder hasBeenInteractedHolder;
    private AudioSource audioSource;


    private void Start()
    {
        First.SetActive(true);
        Second.SetActive(false);

        MainAnimator = GetComponent<Animator>();
        miniGameBool = TriggerObject.GetComponent<MiniGameBool>();
        hasBeenInteractedHolder = TriggerObject.GetComponent<HasBeenInteractedHolder>();
        clickObjects = FindObjectOfType<ClickObjects>();

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

    public void CheckPadlocks()
    {
        Completed = true;
        audioSource.PlayOneShot(soundEffect);


        foreach (Padlock_Tile tile in padlockTiles)
        {
            if (!tile.isCorrect)
            {
                Completed = false;
                break;
            }
        }

        if (Completed)
        {
            First.SetActive(false);
            Second.SetActive(true);

            StartCoroutine(completion());
        }
    }

    private IEnumerator completion()
    {
        hasBeenInteractedHolder.HasBeenInteracted = true;
        miniGameBool.isCompleted = true;
        MainAnimator.SetTrigger("Padlock_Disable");
        yield return new WaitForSeconds(CompletionDelay);
        this.gameObject.SetActive(false);
        clickObjects.CanClick = true;
    }

    public void UIDisable()
    {
        if (!Completed)
        {
            StartCoroutine(disableUI());
        }
    }

    private IEnumerator disableUI()
    {
        MainAnimator.SetTrigger("Padlock_Disable");
        yield return new WaitForSeconds(disableDelay);
        clickObjects.CanClick = true;
        this.gameObject.SetActive(false);

    }
}
