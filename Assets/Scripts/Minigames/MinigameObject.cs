using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameObject : MonoBehaviour
{
    private MiniGameBool miniGameBool;
    private PreRequisite preRequisite;
    private ClickObjects clickObjects;

    [Header("Minigame UI:")]
    [SerializeField] private GameObject GameCanvas;
    [Space(20)]

    [Header("Animations:")]
    [SerializeField] private int disablingAnimationDelay;
    [SerializeField] private GameObject failNotification;
    [SerializeField] private int failNotificationsDelay;
    public GameObject TextFade;

    private bool canProceed;

    private void Start()
    {
        miniGameBool = GetComponent<MiniGameBool>();
        preRequisite = GetComponent<PreRequisite>();
        clickObjects = FindObjectOfType<ClickObjects>();

        GameCanvas.SetActive(false);
    }

    public void MiniGame_Click()
    {
        canProceed = preRequisite.conditionsMet;
        if (canProceed && !miniGameBool.isCompleted)
        {
            GameCanvas.SetActive(true);
        }

        else
        {
            StartCoroutine(cannotProceed());
        }
    }

    private IEnumerator cannotProceed() //Animation for prerequisites not met
    {
        failNotification.SetActive(true);
        TextFade.SetActive(true);
        yield return new WaitForSeconds(failNotificationsDelay);
        clickObjects.CanClick = false;


        if (Input.GetMouseButtonDown(0))
        {
            TextFade.SetActive(false);
            failNotification.SetActive(false);
            clickObjects.CanClick = true;
        }
    }
}