using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreRequisite : MonoBehaviour
{
    public GameObject[] interactableObjects;
    public bool conditionsMet = false;

    public void CheckConditions()
    {
        if (interactableObjects == null || interactableObjects.Length == 0)
        {
            conditionsMet = true;
            return;
        }

        foreach (GameObject obj in interactableObjects)
        {
            InteractHolder holder = obj.GetComponent<InteractHolder>();
            if (holder != null && !holder.HasBeenInteracted)
            {
                conditionsMet = false;
                return;
            }
        }

        conditionsMet = true;
        Debug.Log("All conditions met: " + conditionsMet);
    }
}
