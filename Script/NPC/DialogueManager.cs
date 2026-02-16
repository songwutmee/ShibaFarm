using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    
    public bool IsDialogueActive { get; private set; } 
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake() => Instance = this;

    public void ShowMessage(string message, float duration = 3f)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayRoutine(message, duration));
    }

    public void StartDialogue(DialogueSO dialogue) 
    {
        if (dialogue.lines.Length > 0)
        {
            IsDialogueActive = true;
            ShowMessage(dialogue.lines[0].text, 5f);
        }
    }

    public void DisplayNextSentence() { }

    private IEnumerator DisplayRoutine(string message, float duration)
    {
        IsDialogueActive = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = message;
        yield return new WaitForSeconds(duration);
        dialoguePanel.SetActive(false);
        IsDialogueActive = false;
    }
}