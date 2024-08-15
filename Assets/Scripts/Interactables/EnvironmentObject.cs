using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    private ClickObjects clickObjects;
    private Animator animator;
    private PreRequisite preRequisite;

    private void Awake()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
    }

    private void Start()
    {
      
        preRequisite = GetComponent<PreRequisite>();

        animator = GetComponent<Animator>();
    }

    public void Environment_Click()
    {
        if (clickObjects.CanClick)
        {
            Debug.Log("Test");
            animator.SetTrigger("Go_Animation");
        }
        else
        {
            return;
        }
    }
}
