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

    private Animator SecondAnimator;

    private void Start()
    {
        First.SetActive(true);
        Second.SetActive(false);

        SecondAnimator = Second.GetComponent<Animator>();
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
        SecondAnimator.SetTrigger("Padlock_Disable");
        yield return new WaitForSeconds(CompletionDelay);
        Second.SetActive(false);
    }
}
