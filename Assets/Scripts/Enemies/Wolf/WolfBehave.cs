using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static AlertSign;


public class WolfBehave : MonoBehaviour
{
    public float lives;
    public float speed;
    public float dashSpeed;
    public float dashTime;
    public float longDist;
    public float triggerDist;
    public Transform[] patrolPoints;
    //public AlertSign alert;

    private Rigidbody2D body;
    private Transform target;
    private bool dashFlag = true;
    private bool randFlag = false;
    private int currentPoint = 1;
    private bool restFlag = false;
    private float nextDecisionTime = 0f;
    private float decisionInterval = 1f; // интервал между рандомными решениями
    private Transform _transform;
    //private float xFlip = 1;
    //private float yFlip = 1;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
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
            //if (dir < 0) { _transform.localScale = new Vector2(1, 1); }
            //else { _transform.localScale = new Vector2(-1, 1); }
            body.velocity = new Vector2(dir, body.velocity.y);

            if (Math.Abs(patrolPoints[currentPoint].position.x - transform.position.x) <= 1)
            {
                currentPoint = Math.Abs(currentPoint - 1);
                body.velocity = new Vector2(dir, body.velocity.y);
            }
        }

        // подходим
        else if (Math.Abs(target.position.x - transform.position.x) < longDist && Math.Abs(target.position.x - transform.position.x) > triggerDist && dashFlag)
        {
            dir = CalcDistance(target.position.x, transform.position.x, speed);
            //if (dir < 0) { _transform.localScale = new Vector2(xFlip, yFlip); }
            //else { _transform.localScale = new Vector2(-xFlip, yFlip); }
            body.velocity = new Vector2(dir, body.velocity.y);

        }
        // рывок
        else if (Math.Abs(target.position.x - transform.position.x) <= triggerDist && dashFlag)
        {
            //alert.Show();
            dir = CalcDistance(target.position.x, transform.position.x, dashSpeed);
           // if (dir < 0) { _transform.localScale = new Vector2(xFlip, yFlip); }
            //else { _transform.localScale = new Vector2(-xFlip, yFlip); }
            body.velocity = new Vector2(dir, body.velocity.y);
        }

        // отходит после рывка
        if (!dashFlag && restFlag)
        {
            dir = -CalcDistance(target.position.x, transform.position.x, speed);
           // if (dir < 0) { _transform.localScale = new Vector2(xFlip, yFlip); }
            //else { _transform.localScale = new Vector2(-xFlip, yFlip); }daad
            body.velocity = new Vector2(dir, body.velocity.y);
        }

        // волк отошел достаточно далеко и снова начинает норм поведение
        if (Math.Abs(target.position.x - transform.position.x) >= longDist - 1 && !dashFlag)
        {
            dashFlag = true;
        }

        if (lives <= 0)
        {
            Destroy(gameObject);
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        float TakenDamage;
        switch (col.tag)
        { 
            case "PlayerBullet": //если снаряд игрока
                TakenDamage = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().Damage;
                lives -= TakenDamage;
                //animator.SetTrigger("Damaged");
                break;
            case "PlayerTongue": //если язык игрока
                lives -= 3;
                break;
            case "Weapon": //если оружие
                lives -= 3;
                break;
            case "Player": //если оружие
                dashFlag = false;
                restFlag = false;
                body.velocity = new Vector2(0, body.velocity.y);
                StartCoroutine(DashCounter(dashTime));
                break;
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
