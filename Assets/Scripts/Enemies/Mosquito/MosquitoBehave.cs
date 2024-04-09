using System.Collections;
using UnityEngine;
using System;

public class MosquitoBehave : MonoBehaviour
{
    public float speed;
    public float attackSpeed;
    public float attackDistance;
    public float restTime;
    public float longDistance;
    public float flyingHeight;

    private Rigidbody2D rb;
    private Transform player;
    private bool isAttacking = false;
    private bool isResting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // взлетает на заданную высоту, если игрок далеко
        if (distanceToPlayer > longDistance)
        {
            FlyAtHeight();
        }

        // преследует игрока, если он в пределах long distance
        else {
            if (distanceToPlayer <= longDistance)
            {
                // атакует (летит в игрока), если тот на рассто€нии атаки
                if (distanceToPlayer <= attackDistance && !isAttacking && !isResting)
                {
                    Attack();
                }
                else
                {
                    ChasePlayer();
                }
            }

            else
            {
                Vector2 directionFromPlayer = (transform.position - player.position).normalized;
                rb.velocity = directionFromPlayer * -speed;
            }
        }
    }

    void ChasePlayer()
    {
        float direction = player.position.x > transform.position.x ? 1f : -1f;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void Attack()
    {
        isAttacking = true;
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * attackSpeed;

        //StartCoroutine(PerformAttack());
    }

    void FlyAtHeight()
    {
        // ќпредел€ем направление, в котором должен двигатьс€ комар по оси Y
        float directionY = transform.position.y < flyingHeight ? 1f : -1f;
        // –ассчитываем новую позицию по оси Y
        float newY = transform.position.y + directionY * speed * Time.deltaTime;
        // ≈сли комар близко к заданной высоте, останавливаем его движение
        if (Mathf.Abs(transform.position.y - flyingHeight) < 0.1f)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            // ”станавливаем новую позицию комара
            transform.position = new Vector2(transform.position.x, newY);
        }
    }

    /*IEnumerator PerformAttack()
    {
        *//*        Vector2 directionFromPlayer = (transform.position - player.position).normalized;
                rb.velocity = directionFromPlayer * attackSpeed; // ќтталкиваемс€ от игрока*//*
        isAttacking = true;
        yield return new WaitForSeconds(restTime);
        rb.velocity = Vector2.zero;
        isAttacking = false;
    }*/

    IEnumerator Rest()
    {
        isResting = true;
        yield return new WaitForSeconds(restTime);
        isResting = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Rest());
        }
            
        // нужно добавить эффект вампиризма
    }
}