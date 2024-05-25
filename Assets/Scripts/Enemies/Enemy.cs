using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public float lives;
    private float TakenDamage;
    public float DealtDamage;
    public float KnockbackPower;
    public float AgressDistance = 10f;
    public float Speed = 5f;
    public float RestTime = 2f;
    public float NotifTime = 1f;

    public Transform Target { get; set; }
    public StateMachine SM { get; set; }
    public GameObject Sprite;
    private Rigidbody2D rb;
    public GameObject Notification;

    private GameObject Tongue;
    private GameObject player;
    private GameObject currentWeapon;

    public List<Transform> food = new List<Transform>();

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SM = new StateMachine();
        Tongue = GameObject.FindGameObjectWithTag("PlayerTongue");
        player = GameObject.FindGameObjectWithTag("Player");
        currentWeapon = GameObject.Find("Weapon");
        Notification = transform.GetChild(0).gameObject;
    }

    public virtual void Update()
    {
        if (SM.CurrentState != null)
            SM.CurrentState.Update();

        if (lives <= 0)
        {
            int randomFood = Random.Range(0, food.Count);
            if (Random.Range(0f, 1f) <= food[randomFood].GetComponent<Food>().dropPercent)
            {
                Instantiate(food[randomFood], transform.position, Quaternion.identity);
            }     
            Destroy(gameObject);          
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "PlayerBullet": //если снаряд игрока
                TakenDamage = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().Damage;
                lives -= TakenDamage;
                if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().isPoison)
                {
                    float PoisonDamaged = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().PoisonDamage;
                    int n = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().numOfPosionHits;
                    StartCoroutine(GetPoisoned(n, PoisonDamaged));
                }
                break;
            case "PlayerTongue": //если язык игрока
                TakenDamage = Tongue.GetComponent<TongueAttack>().Damage;
                lives -= TakenDamage;
                if (Tongue.GetComponent<TongueAttack>().Knockback)
                {
                    StartCoroutine(PushAway(Target, 100));
                }
                if (Tongue.GetComponent<TongueAttack>().isPoison)
                {
                    float PoisonDamaged = Tongue.GetComponent<TongueAttack>().PoisonDamage;
                    int n = Tongue.GetComponent<TongueAttack>().numOfPosionHits;
                    StartCoroutine(GetPoisoned(n, PoisonDamaged));
                }
                if (Tongue.GetComponent<TongueAttack>().isVampirism)
                {
                    float percentOfVamp = Tongue.GetComponent<TongueAttack>().percentOfVamp;
                    player.GetComponent<PlayerMovements>().lives += (TakenDamage * percentOfVamp);
                }
                break;
            case "Weapon": //если оружие
                TakenDamage = currentWeapon.transform.GetChild(0).gameObject.GetComponent<Weapon>().Damage;
                lives -= TakenDamage;
                break;
        }
    }

    private IEnumerator GetPoisoned(int n, float PoisonDamaged) //отравление
    {
        Sprite.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
        for (int i = 0; i < n; ++i)
        {
            lives -= PoisonDamaged;
            yield return new WaitForSeconds(0.5f);
        }
        Sprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }

    //откидывание врага
    private IEnumerator PushAway(Transform pushFrom, float pushPower) //нужно у врагов Linear Drag на 5 включать
    {
        float time = 0;
        while (0.1 > time)
        {
            time += Time.deltaTime;
            Vector2 direction = (pushFrom.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * pushPower);
        }
        yield return 0;
    }
}
