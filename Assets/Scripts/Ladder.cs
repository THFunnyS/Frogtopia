using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]

    public float speed = 5;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                col.GetComponent<Rigidbody2D>().gravityScale = 0;
                col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                col.GetComponent<Rigidbody2D>().gravityScale = 0;
                col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            }
            else
            {
                col.GetComponent<Rigidbody2D>().gravityScale = 0;
                col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        col.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
