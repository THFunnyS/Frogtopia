using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArtifactEffects
{
    DJUMP, TEST
}

public class PickableArtifact : MonoBehaviour
{
    public Transform Player;
    public GameObject pickUpButton;
    public ArtifactEffects effects;

    bool pickable = false;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 1.8f
            && Mathf.Abs(transform.position.y - Player.transform.position.y) < 1.8f)
        {
            pickable = true;
            pickUpButton.SetActive(true);
        }
        else
        {
            pickable = false;
            pickUpButton.SetActive(false);
        }
        if (pickable && Input.GetKeyDown(KeyCode.E))
        {
            ApplyEffect();
            Destroy(gameObject);
        }
    }
    void ApplyEffect()
    {
        switch (effects)
        {
            case ArtifactEffects.DJUMP:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>().canDoubleJump = true;
                    break;
                }
            case ArtifactEffects.TEST:
                {
                    break;
                }
        }
    }
}

