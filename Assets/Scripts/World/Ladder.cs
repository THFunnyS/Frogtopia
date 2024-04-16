using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float speed = 5;
    public AudioSource moveSound;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!moveSound.isPlaying) {
                moveSound.Play();
            }
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
                moveSound.Stop();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        col.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        moveSound.Stop();
    }
}
