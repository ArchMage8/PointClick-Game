using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject NextSceneButton;
    [SerializeField] private GameObject LevelLoader;
    [SerializeField] private int destinationScene;
    [SerializeField] private AudioClip soundEffect;
    private AudioSource audioSource;
    private PreRequisite preRequisite;
    private ClickObjects clickObjects;

    

    private void Start()
    {
        preRequisite = GetComponent<PreRequisite>();
        NextSceneButton.SetActive(false);
        clickObjects = FindObjectOfType<ClickObjects>();

        GameObject sfxSourceObject = GameObject.Find("SFXSource");

        if (sfxSourceObject != null)
        {
            audioSource = sfxSourceObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("SFXSource GameObject not found in the scene.");
        }
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
        if (clickObjects.CanClick)
        {
            StartCoroutine(LoadingScene());
        }
    }

    private IEnumerator LoadingScene()
    {
        DecreaseVolume(2f);
        LevelLoader.SetActive(true);
        audioSource.PlayOneShot(soundEffect);
        Animator LevelLoad = LevelLoader.GetComponentInChildren<Animator>();
        LevelLoad.SetTrigger("EndOfScene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(destinationScene);
    }

    public void DecreaseVolume(float duration)
    {
        StartCoroutine(FadeOutVolume(duration));
    }

    private IEnumerator FadeOutVolume(float duration)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;
    }
}
