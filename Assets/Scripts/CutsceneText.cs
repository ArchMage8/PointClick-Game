using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneText : MonoBehaviour
{
    [SerializeField] private string[] Sentences;
    [SerializeField] private string[] colorHexCodes;
    private int Index = 0;
    private bool isTyping = false;
    [SerializeField] private float writeSpeed;
    [SerializeField]private TextMeshProUGUI DialogueText;
    [SerializeField]private int DestinationScene;


    private void Start()
    {
        NextSentence();


        if (Sentences.Length != colorHexCodes.Length)
        {
            Debug.LogError("Length of color and text arrays need to be same");
        }
    }

    private void Update()
    {
        if (Index >= Sentences.Length)
        {
            SceneManager.LoadScene(DestinationScene);
        }

        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextSentence();
        }
    }

    public void NextSentence()
    {
        if (Index <= Sentences.Length - 1 && !isTyping)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
    }


    public IEnumerator WriteSentence()
    {

        foreach (char Character in Sentences[Index].ToCharArray())
        {
            ChangeTextColor(Index);
            DialogueText.text += Character;
            isTyping = true;
            yield return new WaitForSeconds(writeSpeed);
        }
        isTyping = false;
        Index++;
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
}
