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
    public bool canDoubleJump;
    private bool canJump;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask Ground;

    public bool isArmed = false;

    public Image healthBar;
    public AudioClip pain;
    private bool isInvulnerable = false;

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
        if (isGrounded) canJump = true;

        if (Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
            Invoke("IgnoreLayerOff", 0.5f);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canJump && canDoubleJump)
        {
            rb.velocity = Vector2.up * jumpForce;
            canJump = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!isInvulnerable)
        {
            switch (col.tag)
            {
                case "Enemy":
                    TakenDamage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().DealtDamage;
                    lives -= TakenDamage;
                    isInvulnerable = true;
                    AudioSource.PlayClipAtPoint(pain, transform.position);
                    StartCoroutine(setInvulnerability(1.5f));
                    break;
                case "EnemyBullet":
                    TakenDamage = GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<FlyBullet>().Damage;
                    lives -= TakenDamage;
                    isInvulnerable = true;
                    AudioSource.PlayClipAtPoint(pain, transform.position);
                    StartCoroutine(setInvulnerability(1.5f));
                    break;
            }
        }
    }

    private IEnumerator setInvulnerability(float time)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(time);
        isInvulnerable = false;
    }

    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(3, 7, false);
    }
}