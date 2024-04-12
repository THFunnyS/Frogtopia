using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public int lives;
    private int TakenDamage;
    public int DealtDamage;
    public float AgressDistance = 10f;
    public float Speed = 5f;
    public float RestTime = 2f;

    private Transform currentWeapon;
    public Transform Target { get; set; }
    public StateMachine SM { get; set; }

    public GameObject Sprite;

    private Rigidbody2D rb;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SM = new StateMachine();
    }

    public virtual void Update()
    {
        if (SM.CurrentState != null)
            SM.CurrentState.Update();

        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "PlayerBullet":
                TakenDamage = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().Damage;
                lives -= TakenDamage;
                if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().isPoison == true)
                {
                    Sprite.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                    StartCoroutine(GetPoisoned());
                }
                break;
            case "PlayerTongue":
                TakenDamage = GameObject.FindGameObjectWithTag("PlayerTongue").GetComponent<TongueAttack>().Damage;
                lives -= TakenDamage;
                if (GameObject.FindGameObjectWithTag("PlayerTongue").GetComponent<TongueAttack>().Knockback == true)
                {
                    StartCoroutine(PushAway(Target, 100));
                }
                break;
            case "Weapon":
                currentWeapon = GameObject.Find("Weapon").GetComponent<Transform>();
                TakenDamage = currentWeapon.transform.GetChild(0).gameObject.GetComponent<Weapon>().Damage;
                lives -= TakenDamage;
                break;
        }
    }

    private IEnumerator GetPoisoned()
    {
        int PoisonDanaged = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().PoisonDamage;
        int n = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().numOfPosionHits;
        for (int i = 0; i < n; ++i)
        {
            lives -= PoisonDanaged;
            yield return new WaitForSeconds(0.5f);
        }
        Sprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }

    public IEnumerator PushAway(Transform pushFrom, float pushPower)  //нужно у врагов Linear Drag на 5 включать
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
