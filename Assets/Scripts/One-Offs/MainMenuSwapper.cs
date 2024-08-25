using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSwapper : MonoBehaviour
{
    [SerializeField] private GameObject initialObject;
    [SerializeField] private GameObject secondObject;

    [SerializeField] private GameObject LevelLoader;
    [SerializeField] private int DestintationInt;
    [SerializeField] private AudioClip AlarmSoundEffect;
    [SerializeField] private AudioClip ButtonPressSound;
    private Animator LoaderAnimator;
    private AudioSource audioSource;

    private void Start()
    {
        initialObject.SetActive(true);
        secondObject.SetActive(false);

        LoaderAnimator = LevelLoader.GetComponent<Animator>();

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

    public void swapObjects()
    {
        initialObject.SetActive(false);
        secondObject.SetActive(true);
        PlayGame();
    }

    public void PlayGame()
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
            DecreaseVolume(2f);
            audioSource.PlayOneShot(AlarmSoundEffect);
            yield return new WaitForSeconds(1.5f);
            LevelLoader.SetActive(true);
            LoaderAnimator.SetTrigger("EndOfScene");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(DestintationInt);
        }
    }

    public void Quit()
    {
       StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame()
    {
        DecreaseVolume(2f);
        audioSource.PlayOneShot(ButtonPressSound);
        yield return new WaitForSeconds(1.5f);
        LevelLoader.SetActive(true);
        LoaderAnimator.SetTrigger("EndOfScene");
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
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
