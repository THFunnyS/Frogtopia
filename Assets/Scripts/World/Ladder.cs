using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float speed = 5;
    private GameObject moveSound;
    private bool isMoveSound = false;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!isMoveSound) {
                moveSound = AudioManager.PlaySoundLoop(AudioManager.inst.LadderMove);
                isMoveSound = true;
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
                Destroy(moveSound);
                isMoveSound = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        col.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        Destroy(moveSound);
        isMoveSound = false;
    }
}
