using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Padlock_Main : MonoBehaviour
{
    public bool Completed = false;
    public Padlock_Tile[] padlockTiles;
    public GameObject First;
    public GameObject Second;
    public float CompletionDelay = 5f;
    public float disableDelay = 5f;

    private Animator MainAnimator;

    private void Start()
    {
        First.SetActive(true);
        Second.SetActive(false);

        MainAnimator = GetComponent<Animator>();
    }

    public void CheckPadlocks()
    {
        Completed = true;

        foreach (Padlock_Tile tile in padlockTiles)
        {
            if (!tile.isCorrect)
            {
                Completed = false;
                break;
            }
        }

        if (Completed)
        {
            First.SetActive(false);
            Second.SetActive(true);

            StartCoroutine(completion());
        }
    }

    private IEnumerator completion()
    {
        MainAnimator.SetTrigger("Padlock_Disable");
        yield return new WaitForSeconds(CompletionDelay);
        this.gameObject.SetActive(false);
    }

    public void UIDisable()
    {
        if (!Completed)
        {
            StartCoroutine(disableUI());
        }
    }

    private IEnumerator disableUI()
    {
        MainAnimator.SetTrigger("Padlock_Disable");
        yield return new WaitForSeconds(disableDelay);
        this.gameObject.SetActive(false);
    }
}
