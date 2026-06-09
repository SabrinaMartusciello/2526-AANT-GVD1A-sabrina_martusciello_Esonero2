using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text npcNameText;
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button closeButton;

    private string[] currentLines;
    private int currentIndex = 0;
    private bool isTyping = false;

    void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    void Start()
    {
        nextButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(NextLine);
        closeButton.onClick.AddListener(CloseDialogue);
    }

    public void StartDialogue(string name, string[] lines)
    {
        currentLines = lines;
        currentIndex = 0;
        npcNameText.text = name;
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine(lines[0]));
    }

    public void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentLines[currentIndex];
            isTyping = false;
            return;
        }

        currentIndex++;

        if (currentIndex < currentLines.Length)
        {
            StartCoroutine(TypeLine(currentLines[currentIndex]));
        }
        else
        {
            CloseDialogue();
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.04f);
        }

        isTyping = false;
    }

    public void CloseDialogue()
    {
        StopAllCoroutines();
        isTyping = false;
        currentIndex = 0;
        dialoguePanel.SetActive(false);
    }
}