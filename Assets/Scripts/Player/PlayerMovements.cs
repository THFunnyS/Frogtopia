using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    public int lives;
    private int MaxLives;
    private int TakenDamage;
    public float speed;
    public float jumpForce;
    private float moveInput;
    public bool canDoubleJump;
    private bool canJump;

    private Animator anim;

    private bool isFacingRight = true;
    Vector3 pos;
    Camera cam;

    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 0.5f;

    private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask Ground;

    public bool isArmed = false;

    public Image healthBar;
    public AudioClip pain;
    public GameObject DeathPanel;
    private bool isInvulnerable = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
        healthBar = GameObject.Find("HelathBar").GetComponent<Image>();
        //DeathPanel = GameObject.Find("DeathPanel");
        anim = GetComponent<Animator>();
        MaxLives = 15;
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        pos = cam.WorldToScreenPoint(transform.position);
        Flip();

        if (isDashing) return;

        healthBar.fillAmount = (float)lives / MaxLives;
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (lives <= 0) Death();
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
                    anim.SetTrigger("Damaged");
                    isInvulnerable = true;
                    AudioSource.PlayClipAtPoint(pain, transform.position);
                    StartCoroutine(SetInvulnerability(1.5f));
                    break;
                case "EnemyBullet":
                    TakenDamage = GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<FlyBullet>().Damage;
                    lives -= TakenDamage;
                    anim.SetTrigger("Damaged");
                    isInvulnerable = true;
                    AudioSource.PlayClipAtPoint(pain, transform.position);
                    StartCoroutine(SetInvulnerability(1.5f));
                    break;
            }
        }
    }

    void Flip()
    {
        if (Input.mousePosition.x < pos.x && isFacingRight)
        {
            isFacingRight = false;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            cam.transform.localScale = localScale;
        }

        if (Input.mousePosition.x > pos.x && !isFacingRight)
        {
            isFacingRight = true;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            cam.transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(moveInput * dashPower, 0f);
        tr.emitting = true;
        StartCoroutine(SetInvulnerability(0.5f));
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator SetInvulnerability(float time)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(time);
        isInvulnerable = false;
        anim.SetTrigger("DamageGone");
    }

    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(3, 7, false);
    }

    public void Death()
    {
        DeathPanel.SetActive(true);
        speed = 0f;
        isInvulnerable = true;
        anim.SetTrigger("Died");
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Loader.Load(Loader.Scenes.HQ);
        }
    }
}