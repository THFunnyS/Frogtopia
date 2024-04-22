using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    public GameObject Sprite;
    public float lives;
    private int MaxLives;
    private float TakenDamage;

    private float moveInput;
    public float speed;

    private Rigidbody2D rb;
    public float jumpForce;
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

    public bool isArmorSkin = false;
    private float armorResist = 1f;
    private float armorTime = 5f;
    private float armorCooldown = 10f;

    public GameObject PoisonCloud;
    public bool isPoisonCloud = false;
    public float PoisonCloudDamage = 1;
    public int numOfPoisonCloudHits = 3;
    private float PoisonCloudCooldown = 10f;

    public AudioSource moveSound;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
        healthBar = GameObject.Find("HelathBar").GetComponent<Image>();
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

        healthBar.fillAmount = lives / MaxLives; //высчитывание хп дл€ полоски хп
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground);
        if (isGrounded) canJump = true;

        if (Input.GetKeyDown(KeyCode.S)) //спуск с платформы
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
            Invoke("IgnoreLayerOff", 0.5f);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) //прыжок
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canJump && canDoubleJump) //двойной прыжок
        {
            rb.velocity = Vector2.up * jumpForce;
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) //уворот
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.F)) //скиллы тела жабы
        {
            if (isArmorSkin) StartCoroutine(ArmorSkin()); //бронированна€ кожа
            if (isPoisonCloud) StartCoroutine(PoisonCloudSkill());
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.35f && isGrounded) //звуки ходьбы
        {
            //AudioManager.PlaySound(AudioManager.inst.PalyerGrassWalk);
            if (!moveSound.isPlaying) moveSound.Play();

        }
        else
        {
            moveSound.Stop();
        }

        if (lives <= 0) Death(); //смерть
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!isInvulnerable)
        {
            switch (col.tag)
            {
                case "Enemy":
                    TakenDamage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().DealtDamage;
                    lives -= TakenDamage * armorResist;
                    anim.SetTrigger("Damaged");
                    isInvulnerable = true;
                    StartCoroutine(PushedAway(GameObject.FindGameObjectWithTag("Enemy").transform, GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().KnockbackPower));
                    AudioSource.PlayClipAtPoint(pain, transform.position);
                    StartCoroutine(SetInvulnerability(1.5f));
                    break;
                case "EnemyBullet":
                    TakenDamage = GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<FlyBullet>().Damage;
                    lives -= TakenDamage * armorResist;
                    anim.SetTrigger("Damaged");
                    isInvulnerable = true;
                    StartCoroutine(PushedAway(GameObject.FindGameObjectWithTag("EnemyBullet").transform, GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<FlyBullet>().KnockbackPower));
                    AudioSource.PlayClipAtPoint(pain, transform.position);
                    StartCoroutine(SetInvulnerability(1.5f));
                    break;
            }
        }
    }

    void Flip() //поворот моделки игрока
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

    private IEnumerator ArmorSkin() //бронированна€ кожа
    {
        isArmorSkin = false;
        armorResist = 0.7f;
        Sprite.GetComponent<SpriteRenderer>().color = new Color(130, 60, 30);
        yield return new WaitForSeconds(armorTime);
        armorResist = 1f;
        Sprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        yield return new WaitForSeconds(armorCooldown);
        isArmorSkin = true;
    }

    private IEnumerator PoisonCloudSkill()
    {
        Instantiate(PoisonCloud, transform.position, transform.rotation);
        isPoisonCloud = false;
        yield return new WaitForSeconds(PoisonCloudCooldown);
        isPoisonCloud = true;
    }

    private IEnumerator Dash() //уворот
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

    private IEnumerator PushedAway(Transform pushFrom, float pushPower) //откидывание при получении урона
    {
        float time = 0;
        while (0.1 > time)
        {
            //pushFrom.transform.position = new Vector2(pushFrom.transform.position.x, 0);
            //Transform tr = transform;
            //tr.transform.position = new Vector2(this.transform.position.x, 0);
            time += Time.deltaTime;
            Vector2 direction = (pushFrom.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * pushPower);
        }
        yield return 0;
    }

    private IEnumerator SetInvulnerability(float time) //неу€звимость
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(time);
        isInvulnerable = false;
        anim.SetTrigger("DamageGone");
    }

    void IgnoreLayerOff() //игнор слоЄв дл€ спуска с платформы
    {
        Physics2D.IgnoreLayerCollision(3, 7, false);
    }

    public void Death() //смерть
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