using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WolfBehave : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float dashTime;
    public float longDist;
    public float triggerDist;
    public float punchDist;
    public Transform[] patrolPoints; 

    private Rigidbody2D body;
    private Transform target;
    private bool dashFlag = true;
    private int currentPoint = 1;
    private bool restFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dir;
        // player далеко, патрулируем
        if (Math.Abs(target.position.x - transform.position.x) >= longDist && dashFlag)
        {
            dir = CalcDistance(patrolPoints[currentPoint].position.x, transform.position.x, speed);
            body.velocity = new Vector2(dir, body.velocity.y);
            if (Math.Abs(patrolPoints[currentPoint].position.x - transform.position.x) <=1)
            {
                currentPoint = Math.Abs(currentPoint - 1);
                body.velocity = new Vector2(dir, body.velocity.y);
            }
        }

        // подходим
        else if (Math.Abs(target.position.x - transform.position.x) < longDist && Math.Abs(target.position.x - transform.position.x) > triggerDist && dashFlag)
        {
            dir = CalcDistance(target.position.x, transform.position.x, speed);
            body.velocity = new Vector2(dir, body.velocity.y);
        }
        // рывок
        else if (Math.Abs(target.position.x - transform.position.x) <= triggerDist && Math.Abs(target.position.x - transform.position.x) > punchDist && dashFlag)
        {
            dir = CalcDistance(target.position.x, transform.position.x, dashSpeed);
            body.velocity = new Vector2(dir, body.velocity.y);
        }

        // отходим
        /* else
         {
             dir = target.position.x > transform.position.x ? speed : -speed;
             body.velocity = new Vector2(-dir * 5, body.velocity.y);
         }*/

        // надо перенести в OnTriggerEnter2D, но он почему то пока не работает
        if (Math.Abs(target.position.x - transform.position.x) <= punchDist)
        {
            dashFlag = false;
            restFlag = false;
            body.velocity = new Vector2(0, body.velocity.y);
            StartCoroutine(DashCounter(dashTime));
        }

        if (!dashFlag && restFlag)
        {
            dir = CalcDistance(target.position.x, transform.position.x, speed);
            body.velocity = new Vector2(dir, body.velocity.y);
        }

        // волк отошел достаточно далеко
        if (Math.Abs(target.position.x - transform.position.x) >= longDist-1 && !dashFlag)
        {
            dashFlag = true;
        }


    }

    IEnumerator DashCounter(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        restFlag = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            body.velocity = new Vector2(0, body.velocity.y);
            Punch();
        }
    }

    private float CalcDistance(float targetPosition, float objPosition, float objSpeed)
    {
        return (targetPosition > objPosition ? objSpeed : -objSpeed);
    }


    void Punch() { }
}

/* проблемы: 
 * 1) не чувствует столкновение => бесконечно идет, двигает игрока
 */
