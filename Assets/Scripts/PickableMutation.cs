using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableMutation : MonoBehaviour
{
    public Transform Player;
    public string Tag;
    public string script;
    GameObject MutatedPart;


    public GameObject pickUpMutationButton;
    bool pickable = false;


    float t = 0;
    float Offset = 0;
    float Amp = 0.25f;
    float Freq = 2;
    Vector2 StartPos;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        MutatedPart = GameObject.FindGameObjectWithTag(Tag);
        StartPos = transform.position;
    }

    void Update()
    {
        t += Time.deltaTime;
        Offset = Amp * Mathf.Sin(t * Freq);
        transform.position = StartPos + new Vector2(0, Offset);

        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 1.8f)
        {
            pickable = true;
            pickUpMutationButton.SetActive(true);
        }
        else
        {
            pickable = false;
            pickUpMutationButton.SetActive(false);
        }
        PickUp();
    }

    void PickUp()
    {
        if (pickable == true && Input.GetKeyDown(KeyCode.E))
        {
            (MutatedPart.GetComponent(script) as MonoBehaviour).enabled = true;
            Destroy(gameObject);
        }
    }
}
