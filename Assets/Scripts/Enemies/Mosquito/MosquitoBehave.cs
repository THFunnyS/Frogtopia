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

        // �������� �� �������� ������, ���� ����� ������
        if (distanceToPlayer > longDistance)
        {
            FlyAtHeight();
        }

        // ���������� ������, ���� �� � �������� long distance
        else {
            if (distanceToPlayer <= longDistance)
            {
                // ������� (����� � ������), ���� ��� �� ���������� �����
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
        // ���������� �����������, � ������� ������ ��������� ����� �� ��� Y
        float directionY = transform.position.y < flyingHeight ? 1f : -1f;
        // ������������ ����� ������� �� ��� Y
        float newY = transform.position.y + directionY * speed * Time.deltaTime;
        // ���� ����� ������ � �������� ������, ������������� ��� ��������
        if (Mathf.Abs(transform.position.y - flyingHeight) < 0.1f)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            // ������������� ����� ������� ������
            transform.position = new Vector2(transform.position.x, newY);
        }
    }

    /*IEnumerator PerformAttack()
    {
        *//*        Vector2 directionFromPlayer = (transform.position - player.position).normalized;
                rb.velocity = directionFromPlayer * attackSpeed; // ������������� �� ������*//*
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
            
        // ����� �������� ������ ����������
    }
}