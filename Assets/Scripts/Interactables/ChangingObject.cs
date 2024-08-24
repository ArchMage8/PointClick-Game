using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingObject : MonoBehaviour
{
    private HasBeenInteractedHolder hasBeenInteractedHolder;
    private PreRequisite preRequisite;
    private ClickObjects clickObjects;

    [Header("Changing Objects:")]
    [SerializeField]private GameObject FirstObject;
    [SerializeField]private GameObject SecondObject;
    [Space(20)]

    [Header("Animations:")]
    [SerializeField]private GameObject FailNotification;
    [SerializeField] private int failAnimationDelay;
    [SerializeField] private int enablingAnimationDelay;
    public GameObject TextFade;
    [Space(20)]

    private bool CanProceed;

    private void Start()
    {
        hasBeenInteractedHolder = GetComponent<HasBeenInteractedHolder>();
        preRequisite = GetComponent<PreRequisite>();

        preRequisite.CheckConditions();             //Check if prerequisites are met
        CanProceed = preRequisite.conditionsMet;
        
        clickObjects = FindObjectOfType<ClickObjects>();

        //Deactivating Stuffs
        FailNotification.SetActive(false);
        SecondObject.SetActive(false);
    }
    
    public void change_click()  //Call from the click objects system
    {
        preRequisite.CheckConditions();
        CanProceed = preRequisite.conditionsMet;
        if (CanProceed)
        {
            FirstObject.SetActive(false);
            StartCoroutine(enablingAnimation());
            hasBeenInteractedHolder.HasBeenInteracted = true;
        }

        else if(CanProceed == false)
        {
            Debug.Log("Fail");
            StartCoroutine(cannotProceed());

            
        }
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

    private IEnumerator enablingAnimation() //Animation for object being enabled
    {
        yield return new WaitForSeconds(enablingAnimationDelay);
        SecondObject.SetActive(true);
        clickObjects.CanClick = true;
             
    }
}
