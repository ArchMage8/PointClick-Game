using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    private ClickObjects clickObjects;

    [HideInInspector] public string[] sentences;
    [HideInInspector] public string[] colorHexCodes;

    [HideInInspector] public int Index = 0;
    private bool isTyping = false;
    [SerializeField] private float writeSpeed;

    private void Start()
    {
        clickObjects = FindObjectOfType<ClickObjects>();
        dialogueText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (dialogueText.isActiveAndEnabled && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            if (Index >= sentences.Length)
            {
                StartCoroutine(DisableDialogue());
            }
        }
    }

    public void NextSentence()
    {
        if (Index < sentences.Length && !isTyping)
        {
            dialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
    }

    public IEnumerator WriteSentence()
    {
        if (isTyping)
            yield break;

        isTyping = true;
        dialogueText.text = "";
        foreach (char character in sentences[Index].ToCharArray())
        {
            ChangeTextColor(Index);
            dialogueText.text += character;
            yield return new WaitForSeconds(writeSpeed);
        }
        isTyping = false;
        Index++;
    }

    public void ReceiveDialogue(string[] sentSentences, string[] colors)
    {
        sentences = sentSentences;
        colorHexCodes = colors;
    }

    private IEnumerator DisableDialogue()
    {
        yield return new WaitForSeconds(0f);
        dialogueText.text = "";
        clickObjects.CanClick = true;
        Index = 0;
        sentences = new string[0];
        gameObject.SetActive(false);
    }

    private void ChangeTextColor(int index)
    {
        if (index >= 0 && index < colorHexCodes.Length && ColorUtility.TryParseHtmlString("#" + colorHexCodes[index], out Color newColor))
        {
            dialogueText.color = newColor;
        }
        else
        {
            dialogueText.color = Color.white;
        }
    }
}
