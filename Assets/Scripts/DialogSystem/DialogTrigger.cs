using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public Transform Player;
    public GameObject DialogPanel;
    public GameObject DialogButton;
    private bool canTalk = false;

    public Image NPC_Image;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
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
            NPC_Image.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            TriggerDialog();
        }
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog, DialogPanel);
    }
}
