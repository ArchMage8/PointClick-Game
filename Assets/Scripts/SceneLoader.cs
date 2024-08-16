using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject NextSceneButton;
    [SerializeField] private GameObject LevelLoader;
    [SerializeField] private int destinationScene;
    private PreRequisite preRequisite;

    private void Start()
    {
        preRequisite = GetComponent<PreRequisite>();
        NextSceneButton.SetActive(false);
    }

    private void FixedUpdate()
    {
        preRequisite.CheckConditions();
        
        if (preRequisite.conditionsMet == true) 
        {
            NextSceneButton.SetActive(true);
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene()
    {
        Animator LevelLoad = LevelLoader.GetComponentInChildren<Animator>();
        LevelLoad.SetTrigger("EndOfScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(destinationScene);
    }
}
