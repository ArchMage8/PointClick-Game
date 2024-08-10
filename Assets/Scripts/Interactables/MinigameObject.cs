using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameObject : MonoBehaviour
{
    private HasBeenInteractedHolder hasBeenInteractedHolder;
    private PreRequisite preRequisite;
    private ClickObjects clickObjects;
    public MiniGameBool miniGameBool;

    private bool CanProceed;
    private bool isCompleted;

    private void Start()
    {
        hasBeenInteractedHolder = GetComponent<HasBeenInteractedHolder>();
        preRequisite = GetComponent<PreRequisite>();

        preRequisite.CheckConditions();             //Check if prerequisites are met
        CanProceed = preRequisite.conditionsMet;
        clickObjects = FindObjectOfType<ClickObjects>();
        isCompleted = miniGameBool.isCompleted;

        miniGameBool.gameObject.SetActive(false);
    }

    public void Minigame_Interact()
    {
        if(CanProceed && !isCompleted)
        {
            clickObjects.CanClick = false;
            miniGameBool.gameObject.SetActive(true);
        }
    }

}
