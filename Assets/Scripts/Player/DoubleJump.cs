using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isGrounded;
    private Transform feetPos;
    private float checkRadius;
    private LayerMask Ground;

    private bool canJump;
    private bool canDoubleJump = true;
    private float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpForce = GetComponent<PlayerMovements>().jumpForce;
        feetPos = GetComponent<PlayerMovements>().feetPos;
        checkRadius = GetComponent<PlayerMovements>().checkRadius;
        Ground = GetComponent<PlayerMovements>().Ground;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground);
        if (isGrounded) canJump = true;
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canJump && canDoubleJump)
        {
            rb.velocity = Vector2.up * jumpForce;
            canJump = false;
        }
    }
}
