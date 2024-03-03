using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehave : MonoBehaviour
{
    public Transform[] spots;
    private int randomSpot;
    private float waitTime;
    public float startWaitTime;
    public float speed;

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, spots.Length);
    }

    void Update()
    {  
        transform.position = Vector2.MoveTowards(transform.position, spots[randomSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, spots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
                randomSpot = Random.Range(0, spots.Length);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }        
    }
}
