using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogSystem : MonoBehaviour
{
    public Transform Player;
    public GameObject DialogPanel;
    public GameObject DialogButton;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI NPC_NameText;
    public string NPC_Name;
    public Image NPCimage;
    public string[] lines;
    public float textSpeed;

    private bool canTalk = false;
    private int index = 0;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        textComponent.text = "";
        NPCimage.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        NPC_NameText.text = NPC_Name;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 1.8f && Mathf.Abs(transform.position.y - Player.transform.position.y) < 1.8f)
        {
            canTalk = true;
            DialogButton.SetActive(true);
        }
        else
        {
            canTalk = false;
            DialogButton.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && canTalk == true)
        {

            if (DialogPanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                Player.GetComponent<PlayerMovements>().speed = 0f;
                DialogPanel.SetActive(true);
                StartCoroutine(TypeLine());
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else if (textComponent.text != lines[index])
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
           
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            ZeroText();
        }
    }

    public void ZeroText()
    {
        textComponent.text = "";
        index = 0;
        DialogPanel.SetActive(false);
        Player.GetComponent<PlayerMovements>().speed = 9f;
    }
}
