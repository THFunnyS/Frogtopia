using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingR = true;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask Ground;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        //if (facingR == false && moveInput > 0)
        //{
        //    Flip();
        //}
        //else if (facingR == true && moveInput < 0)
        //{
        //    Flip();
        //}
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) { rb.velocity = Vector2.up * jumpForce; }
    }

    void Flip()
    {
        facingR = !facingR;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
