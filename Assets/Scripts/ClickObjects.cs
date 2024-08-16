using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickObjects : MonoBehaviour
{
   [HideInInspector] public static ClickObjects Instance { get; private set; }
   [HideInInspector] public bool CanClick = true;
   
    private ChangingObject changingObject;
    private InteractObject interactObject;
    private MinigameObject minigameObject;
    private EnvironmentObject environmentObject;

    private PreRequisite preRequisite;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanClick && !EventSystem.current.IsPointerOverGameObject())
        {

           
            DetectClickedObject();
        }
    }

    void DetectClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;
            
            ClickHandler(clickedObject);
           
        }
    }

    private void ClickHandler(GameObject ClickedObject)
    {
        
        preRequisite = ClickedObject.GetComponent<PreRequisite>();
       

        if (preRequisite != null)
        {
            preRequisite.CheckConditions();

            if (preRequisite.conditionsMet)
            {
                if (ClickedObject.CompareTag("Change"))
                {
                    CanClick = false;
                    changingObject = ClickedObject.GetComponent<ChangingObject>();
                    changingObject.change_click();

                    //Logic for objects that change on click
                }

                else if (ClickedObject.CompareTag("Interact"))
                {
                    CanClick = false;
                    interactObject = ClickedObject.GetComponent<InteractObject>();
                    interactObject.Click_Interact();

                    //Logic for objects that enable dialogue on click
                }

                else if (ClickedObject.CompareTag("Minigame"))
                {
                    CanClick = false;
                    minigameObject = ClickedObject.GetComponent<MinigameObject>();
                    minigameObject.MiniGame_Click();
                    //Logic for objects that enable the minigame on click
                }

                else if (ClickedObject.CompareTag("Environment"))
                {
                    

                    environmentObject = ClickedObject.GetComponent<EnvironmentObject>();
                    environmentObject.Environment_Click();
                    
                    //Logic for objects that are environment animated
                }

                else
            {
                CanClick = true;
                //Non interactable object clicked
                return;
            }
            }
        }
        else
        {
            return;
        }
    }
}
