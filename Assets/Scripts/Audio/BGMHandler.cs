using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMHandler : MonoBehaviour
{
    private static BGMHandler instance;
    private bool hasInitialized = false;

    [SerializeField]
    private int destroyOnSceneIndex;
    [SerializeField]
    private float volumeIncreaseDuration = 5.0f;

    private AudioSource bgmSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (!hasInitialized)
            {
                Initialize();
                bgmSource = GetComponent<AudioSource>();
                if (bgmSource != null)
                {
                    StartCoroutine(GraduallyIncreaseVolume());
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        hasInitialized = true;
    }

    private IEnumerator GraduallyIncreaseVolume()
    {
        float initialVolume = 0f;
        float targetVolume = bgmSource.volume;
        bgmSource.volume = initialVolume;

        float elapsedTime = 0f;

        while (elapsedTime < volumeIncreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(initialVolume, targetVolume, elapsedTime / volumeIncreaseDuration);
            yield return null;
        }

        bgmSource.volume = targetVolume;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == destroyOnSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
