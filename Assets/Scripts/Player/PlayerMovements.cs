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

    public bool canDash = true;
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
    public GameObject DeathPanel;
    private bool isInvulnerable = false;
    
    public float skillTime;
    public float skillCooldown;
    public GameObject SkillSwaper;

    public bool isArmorSkin = true;
    private float armorResist = 1f;
    private float armorTime = 5f;
    private float armorCooldown = 10f;

    public GameObject PoisonCloud; //�������� ������
    private bool isPoisonCloud = true;
    public float PoisonCloudDamage = 1;
    public int numOfPoisonCloudHits = 3;
    private float PoisonCloudCooldown = 10f;

    public GameObject ElectroWave; //������� �����
    private bool canElectroWave = true;
    public float ElectroWaveDamage = 6f;
    private float ElectroWaveCooldown = 10f;

    private GameObject stepSound;
    private bool isStepSound = false;
    private bool isFallSound = false;

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

        healthBar.fillAmount = lives / MaxLives; //������������ �� ��� ������� ��
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground);
        if (isGrounded) canJump = true;

        if (!isGrounded) isFallSound = false;

        if (isGrounded && !isFallSound)
        {
            AudioManager.PlaySound(AudioManager.inst.Landing);
            isFallSound = true;
        }

        if (Input.GetKeyDown(KeyCode.S)) //����� � ���������
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
            Invoke("IgnoreLayerOff", 0.5f);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) //������
        {
            AudioManager.PlaySound(AudioManager.inst.Jump);
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canJump && canDoubleJump) //������� ������
        {
            AudioManager.PlaySound(AudioManager.inst.Jump);
            rb.velocity = Vector2.up * jumpForce;
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) //������
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.F)) //������ ���� ����
        {
            ActivateSkill(SkillSwaper.GetComponent<SkillSwaper>().CurrentSkill());

            //if (isArmorSkin) StartCoroutine(ArmorSkin()); //������������� ����
            //if (isPoisonCloud) StartCoroutine(PoisonCloudSkill()); //�������� ������
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.35f && isGrounded) //����� ������
        {
            if (!isStepSound) { 
                stepSound = AudioManager.PlaySoundLoop(AudioManager.inst.StepSound);
                isStepSound = true;
            }
        }
        else
        {
                Destroy(stepSound);
                isStepSound = false;
        }

        if (lives <= 0) Death(); //������
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
                    AudioManager.PlaySound(AudioManager.inst.PlayerDamage);
                    StartCoroutine(SetInvulnerability(1.5f));
                    break;
                case "EnemyBullet":
                    TakenDamage = GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<FlyBullet>().Damage;
                    lives -= TakenDamage * armorResist;
                    anim.SetTrigger("Damaged");
                    isInvulnerable = true;
                    StartCoroutine(PushedAway(GameObject.FindGameObjectWithTag("EnemyBullet").transform, GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<FlyBullet>().KnockbackPower));
                    AudioManager.PlaySound(AudioManager.inst.PlayerDamage);
                    StartCoroutine(SetInvulnerability(1.5f));
                    break;
            }
        }
    }

    void Flip() //������� ������� ������
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

        void ActivateSkill(string skillName)
    {
        switch (skillName)
        {
            case "PoisonCloud":
                skillTime = 0f;
                skillCooldown = PoisonCloudCooldown;
                if (isPoisonCloud) StartCoroutine(PoisonCloudSkill());
                break;
            case "ArmorSkin":
                skillTime = armorTime;
                skillCooldown = armorCooldown;
                if (isArmorSkin) StartCoroutine(ArmorSkin());
                break;
            case "ElectroWave":
                skillTime = 0f;
                skillCooldown = ElectroWaveCooldown;
                if (canElectroWave) StartCoroutine(ElectroWaveSkill());
                break;
        }
    }

    public void IncArmor(){
        armorResist-=0.3f;
    }

        public void IncHealth(){
        MaxLives += 5;
    }
    public int GetMaxLives(){
        return MaxLives;
    }
    private IEnumerator ArmorSkin() //������������� ����
    {
        isArmorSkin = false;
        armorResist -= 0.3f;
        Sprite.GetComponent<SpriteRenderer>().color = new Color(130, 60, 30);
        yield return new WaitForSeconds(armorTime);
        armorResist += 0.3f;
        Sprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        yield return new WaitForSeconds(armorCooldown);
        isArmorSkin = true;
    }

    private IEnumerator PoisonCloudSkill() //�������� ������
    {
        Instantiate(PoisonCloud, transform.position, transform.rotation);
        isPoisonCloud = false;
        yield return new WaitForSeconds(PoisonCloudCooldown);
        isPoisonCloud = true;
    }

    private IEnumerator ElectroWaveSkill() //������� �����
    {
        Instantiate(ElectroWave, transform.position, transform.rotation);
        canElectroWave = false;
        yield return new WaitForSeconds(ElectroWaveCooldown);
        canElectroWave = true;
    }

    private IEnumerator Dash() //������
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

    private IEnumerator PushedAway(Transform pushFrom, float pushPower) //����������� ��� ��������� �����
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

    private IEnumerator SetInvulnerability(float time) //������������
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(time);
        isInvulnerable = false;
        anim.SetTrigger("DamageGone");
    }

    void IgnoreLayerOff() //����� ���� ��� ������ � ���������
    {
        Physics2D.IgnoreLayerCollision(3, 7, false);
    }

    public void Death() //������
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