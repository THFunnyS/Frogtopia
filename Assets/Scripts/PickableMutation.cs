using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Effects
{
    /*BODY  */  DJUMP,
    /*TONGUE*/ Knockback, PosionShot
}

public class PickableMutation : MonoBehaviour
{
    public Transform Player;
    public Effects effects;

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
        StartPos = transform.position;
    }

    void Update()
    {
        t += Time.deltaTime;
        Offset = Amp * Mathf.Sin(t * Freq);
        transform.position = StartPos + new Vector2(0, Offset);

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
        if (pickable == true && Input.GetKeyDown(KeyCode.E))
        {
            switch (effects)
            {
                case Effects.DJUMP:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>().canDoubleJump = true;
                    break;
                case Effects.Knockback:
                    GameObject.FindGameObjectWithTag("PlayerTongue").GetComponent<TongueAttack>().Knockback = true;
                    break;
                case Effects.PosionShot:
                    GameObject.FindGameObjectWithTag("PlayerTongue").GetComponent<TongueShot>().isPoison = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
