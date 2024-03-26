using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static AlertSign;


public class FoxBehave : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float dashTime;
    public float longDist;
    public float lieDist;
    public float triggerDist;
    public Transform[] patrolPoints;
    public AlertSign alert1;
    public AlertSign alert2;

    private Rigidbody2D body;
    private Transform target;
    private bool dashFlag = true;
    private bool randFlag = false;
    private int currentPoint = 1;
    private bool restFlag = false;
    private float nextDecisionTime = 0f;
    private float decisionInterval = 1f; // интервал между рандомными решениями

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        nextDecisionTime = Time.time + UnityEngine.Random.Range(0f, decisionInterval);
    }

    // Update is called once per frame
    void Update()
    {
        float dir;

        // player далеко, патрулируем
        if (Math.Abs(target.position.x - transform.position.x) >= longDist && dashFlag)
        {
            if (Time.time >= nextDecisionTime)
            {
                MakeRandomDecision();
                nextDecisionTime = Time.time + decisionInterval;
            }
            dir = CalcDistance(patrolPoints[currentPoint].position.x, transform.position.x, speed);
            body.velocity = new Vector2(dir, body.velocity.y);
            if (Math.Abs(patrolPoints[currentPoint].position.x - transform.position.x) <= 1)
            {
                currentPoint = Math.Abs(currentPoint - 1);
                body.velocity = new Vector2(dir, body.velocity.y);
            }
        }

        // подходим
        else if (Math.Abs(target.position.x - transform.position.x) < longDist && Math.Abs(target.position.x - transform.position.x) > lieDist && dashFlag)
        {
            dir = CalcDistance(target.position.x, transform.position.x, speed);
            body.velocity = new Vector2(dir, body.velocity.y);
        }

        // ложный рывок
        else if (Math.Abs(target.position.x - transform.position.x) <= lieDist && Math.Abs(target.position.x - transform.position.x) > triggerDist && dashFlag)
        {
            alert1.Show();
            dir = CalcDistance(target.position.x, transform.position.x, speed);
            body.velocity = new Vector2(dir, body.velocity.y);
        }

        // рывок
        else if (Math.Abs(target.position.x - transform.position.x) <= triggerDist && dashFlag)
        {
            alert2.Show();
            dir = CalcDistance(target.position.x, transform.position.x, dashSpeed);
            body.velocity = new Vector2(dir, body.velocity.y);
        }

        // отходит после рывка
        if (!dashFlag && restFlag)
        {
            dir = -CalcDistance(target.position.x, transform.position.x, speed);
            body.velocity = new Vector2(dir, body.velocity.y);
        }

        // волк отошел достаточно далеко и снова начинает норм поведение
        if (Math.Abs(target.position.x - transform.position.x) >= longDist - 1 && !dashFlag)
        {
            dashFlag = true;
        }


    }

    IEnumerator DashCounter(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        restFlag = true;
    }

    private float CalcDistance(float targetPosition, float objPosition, float objSpeed)
    {
        return (targetPosition > objPosition ? objSpeed : -objSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            alert1.Hide();
            alert2.Hide();
            dashFlag = false;
            restFlag = false;
            body.velocity = new Vector2(0, body.velocity.y);
            StartCoroutine(DashCounter(dashTime));
        }
    }

    // волк рандомно останавливается/поворачивается
    void MakeRandomDecision()
    {
        if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
        {
            StartCoroutine(StopForRandomTime());
        }
        else
        {
            currentPoint = UnityEngine.Random.Range(0, patrolPoints.Length);
        }
    }

    // не уверена работает ли вообще
    IEnumerator StopForRandomTime()
    {
        randFlag = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
        randFlag = false;
    }
}
