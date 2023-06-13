using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text dialogueText;
    public GameObject dialogueBox;
    private Queue<string> sentences;


    private void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) & dialogueBox.active)
        {
            DisplayNextSentence();
        }
    }

    public void StarDialogue(string[] dialogue)
    {
        GameManager.Instance.TogglePause(true);
        dialogueBox.SetActive(true);
        sentences.Clear();
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        GameManager.Instance.TogglePause(true);
    }

}
