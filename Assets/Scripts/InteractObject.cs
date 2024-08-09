using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public DialogueController dialogueController;
    public string[] Sentences;

    public void startDialogue()
    {
        dialogueController.Index = 0;
        dialogueController.RecieveDialogue(Sentences);
    }
}
