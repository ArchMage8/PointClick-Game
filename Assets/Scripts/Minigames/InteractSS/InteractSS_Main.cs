using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSS_Main : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private GameObject TriggerObject;
    [SerializeField] private int CompletionDelay;
    [SerializeField] private int disableDelay;
    private MiniGameBool miniGameBool;
    private ClickObjects clickObjects;
    private HasBeenInteractedHolder hasBeenInteractedHolder;
    private HasBeenInteractedHolder solutionCondition;

    [Space (25)]
    [Header("SlideSho")]
    [SerializeField] private GameObject[] Phases;
    [SerializeField] private GameObject solutionPreReq;


    private int currentIndex = 0;

    private void Start()
    {
        miniGameBool = TriggerObject.GetComponent<MiniGameBool>();
        clickObjects = FindObjectOfType<ClickObjects>();
        hasBeenInteractedHolder = TriggerObject.GetComponent<HasBeenInteractedHolder>();

        for (int i = 0; i < Phases.Length; i++)
        {
            Phases[i].SetActive(i == currentIndex);
        }

        if (Phases.Length > 0)
        {
            Phases[0].SetActive(true);
        }
    }

    public void InteractSS_Handler()
    {
        solutionCondition = solutionPreReq.GetComponent<HasBeenInteractedHolder>();

        if (solutionCondition.HasBeenInteracted)
        {
            OnButtonPressed();
        }
    }

    private void OnButtonPressed()
    {
        if (currentIndex < Phases.Length)
        {
            Phases[currentIndex].SetActive(false);
        }

        currentIndex++;

        if (currentIndex < Phases.Length)
        {
            Phases[currentIndex].SetActive(true);
        }
        else
        {
            if (currentIndex > 0)
            {
                StartCoroutine(completion());
            }
        }
    }

    private IEnumerator completion()
    {
        hasBeenInteractedHolder.HasBeenInteracted = true;
        miniGameBool.isCompleted = true;
        yield return new WaitForSeconds(CompletionDelay);
        this.gameObject.SetActive(false);
        clickObjects.CanClick = true;
    }

    public void UIDisable()
    {
        if (!miniGameBool.isCompleted)
        {
            StartCoroutine(disableUI());
        }
    }

    private IEnumerator disableUI()
    {
       
        yield return new WaitForSeconds(disableDelay);
        clickObjects.CanClick = true;
        this.gameObject.SetActive(false);

    }

}
