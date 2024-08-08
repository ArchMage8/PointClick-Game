using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObjects : MonoBehaviour
{

   [HideInInspector] public bool CanClick = true;

    void Update()
    {
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
        if (ClickedObject.CompareTag("Changeable"))
        {
            //Logic for objects that change on click
        }

        else if (ClickedObject.CompareTag("Interactable"))
        {
            //Logic for objects that enable dialogue on click
        }

        else if (ClickedObject.CompareTag("Minigame"))
        {
            //Logic for objects that enable the minigame on click
        }

        else
        {
            //Non interactable object clicked
            return;
        }
    }
}
