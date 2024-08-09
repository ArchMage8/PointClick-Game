using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingObject : MonoBehaviour
{
    private InteractHolder interactHolder;
    private PreRequisite preRequisite;
    private ClickObjects clickObjects;

    [SerializeField]private GameObject FirstObject;
    [SerializeField]private GameObject SecondObject;

    [SerializeField]private GameObject FailNotification;
    [SerializeField] private int failAnimation;
    [SerializeField] private int enablingAnimationDelay;

    private bool CanProceed;

    private void Start()
    {
        interactHolder = GetComponent<InteractHolder>();
        preRequisite = GetComponent<PreRequisite>();

        preRequisite.CheckConditions();             //Check if prerequisites are met
        CanProceed = preRequisite.conditionsMet;

        clickObjects = FindObjectOfType<ClickObjects>();
    }
    
    public void change_click()  //Call from the click objects system
    {
        if (CanProceed)
        {
            FirstObject.SetActive(false);
            StartCoroutine(enablingAnimation());
        }

        else
        {
            StartCoroutine(cannotProceed());
        }
    }

    private IEnumerator cannotProceed() //Animation for prerequisites not met
    {
        FailNotification.SetActive(true);
        yield return new WaitForSeconds(failAnimation);
        FailNotification.SetActive(false);
    }

    private IEnumerator enablingAnimation() //Animation for object being enabled
    {
        yield return new WaitForSeconds(enablingAnimationDelay);
        SecondObject.SetActive(true);
        clickObjects.CanClick = true;
             
    }
}
