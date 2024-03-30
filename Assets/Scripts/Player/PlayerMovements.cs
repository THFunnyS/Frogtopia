using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    public int lives;
    private int TakenDamage;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask Ground;

    public bool isArmed = false;

    public Image healthBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        healthBar.fillAmount = (float)lives / 10;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) { rb.velocity = Vector2.up * jumpForce; }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Enemy":
                TakenDamage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().DealtDamage;
                lives -= TakenDamage;
                break;
            case "EnemyBullet":
                TakenDamage = GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<FlyBullet>().Damage;
                lives -= TakenDamage;
                break;
        }
    }
}