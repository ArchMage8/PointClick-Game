using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueController : MonoBehaviour
{
    private TextMeshProUGUI DialogueText;
    private ClickObjects clickObjects;

    [HideInInspector] public string[] Sentences;
    [HideInInspector] public string[] colorHexCodes;

    [HideInInspector] public int Index = 0;
    private bool isTyping = false;
    [SerializeField] private float writeSpeed;

    private IEnumerator Holder;
    private bool canProceed = true;

    private void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
        Transform DialogueTextObject = transform.GetChild(0);
        DialogueText = DialogueTextObject.GetComponent<TextMeshProUGUI>();

       
    }

    private void Update()
    {
        if (DialogueText.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && canProceed)
            {
                if (Index >= Sentences.Length)
                {
                    Debug.Log("End");
                    StartCoroutine(DisableThis());
                }
            }
            else
            {
                return;
            }
        }
    }

    public void NextSentence()
    {
        if (Index <= Sentences.Length - 1 && !isTyping)
        {
            canProceed = false;
            //DialogueText.text = "";

            Holder = WriteSentence();
            StartCoroutine(Holder);
        }
        else if (isTyping)
        {
            StartCoroutine(ShowFull());
        }
    }

    public IEnumerator WriteSentence()
    {
        isTyping = true;
        DialogueText.text = "";
        
        foreach (char character in Sentences[Index].ToCharArray())
        {
            ChangeTextColor(Index);
            DialogueText.text += character;
            yield return new WaitForSeconds(writeSpeed);
        }
        isTyping = false;
        canProceed = true;
        Index++;
    }

    public void RecieveDialogue(string[] SentSentences)
    {
        Sentences = new string[SentSentences.Length];
        for (int i = 0; i < SentSentences.Length; i++)
        {
            Sentences[i] = SentSentences[i];
        }
    }

    public void RecieveColors(string[] colors)
    {
        colorHexCodes = new string[Sentences.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colorHexCodes[i] = colors[i];
        }
    }

    IEnumerator DisableThis()
    {
        yield return new WaitForSeconds(0f);

        DialogueText.text = "";
        clickObjects.CanClick = true;
        Index = 0;
        Sentences = new string[0];
        this.gameObject.SetActive(false);
    }

    private void ChangeTextColor(int index)
    {
        if (index >= 0 && index < colorHexCodes.Length)
        {
            string hexCode = colorHexCodes[index];

            if (string.IsNullOrEmpty(hexCode))
            {
                DialogueText.color = Color.white;
            }
            else if (ColorUtility.TryParseHtmlString("#" + hexCode, out Color newColor))
            {
                DialogueText.color = newColor;
            }
        }
    }

    private IEnumerator ShowFull()
    {

        StopCoroutine(Holder);
        DialogueText.text = "";
        DialogueText.text = Sentences[Index];
        isTyping = false;
        Index++;

        yield return new WaitForSeconds(0.1f);
        canProceed = true;
    }
}
