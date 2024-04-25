using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject Player;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialogue, GameObject dialogPanel)
    {
        Player.GetComponent<PlayerMovements>().speed = 0f;
        Player.GetComponent<PlayerMovements>().jumpForce = 0f;
        Player.GetComponent<PlayerMovements>().canDash = false;      
        dialogPanel.SetActive(true);
        nameText.text = dialogue.Name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(dialogPanel);
    }

    public void DisplayNextSentence(GameObject dialogPanel)
    {
        if (sentences.Count == 0) 
        {
            EndDialog(dialogPanel);
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialog(GameObject dialogPanel)
    {
        dialogPanel.SetActive(false);
        Player.GetComponent<PlayerMovements>().speed = 9f;
        Player.GetComponent<PlayerMovements>().jumpForce = 10f;
        Player.GetComponent<PlayerMovements>().canDash = true;
    }
}
