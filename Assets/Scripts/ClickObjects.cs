using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObjects : MonoBehaviour
{
   [HideInInspector] public static ClickObjects Instance { get; private set; }
   [HideInInspector] public bool CanClick = true;
   
    private ChangingObject changingObject;
    private InteractObject interactObject;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        Debug.Log(CanClick);

        if (Input.GetMouseButtonDown(0) && CanClick)
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
            //Logic for objects that enable the minigame on click
        }

        else
        {
            CanClick = true;
            //Non interactable object clicked
            return;
        }
    }
}
