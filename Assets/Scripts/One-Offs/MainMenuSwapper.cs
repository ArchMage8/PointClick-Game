using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject initialObject;
    [SerializeField] private GameObject secondObject;

    [SerializeField] private GameObject LevelLoader;
    [SerializeField] private int DestintationInt;
    private Animator LoaderAnimator;

    private void Start()
    {
        initialObject.SetActive(true);
        secondObject.SetActive(false);

        LoaderAnimator = LevelLoader.GetComponent<Animator>();
    }

    public void swapObjects()
    {
        initialObject.SetActive(false);
        secondObject.SetActive(true);
        playTheGame();
    }

    private void PlayGame()
    {
        StartCoroutine(playTheGame());
    }

    private IEnumerator playTheGame()
    {
        if (LevelLoader == null)
        {
            Debug.LogError("Ask Darryl/Hans to setup the level loader");
            yield return null;
        }

        else
        {
            LoaderAnimator.SetTrigger("EndOfScene");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(DestintationInt);
        }
    }
}