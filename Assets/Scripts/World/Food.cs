using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject Player;
    float healthRegen;
    public float healthRegenPercent;
    public float dropPercent;

    public GameObject pickUpMutationButton;
    bool pickable = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        healthRegen = Player.GetComponent<PlayerMovements>().MaxLives * healthRegenPercent;
        Physics2D.IgnoreCollision(Player.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(), true);
    }

    
    void Update()
    {
        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 1.8f && Mathf.Abs(transform.position.y - Player.transform.position.y) < 1.8f)
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
        if (pickable && Input.GetKeyDown(KeyCode.E))
        {
            Player.GetComponent<PlayerMovements>().lives += healthRegen;
            Destroy(gameObject);
        }
    }
}
