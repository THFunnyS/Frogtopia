using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public int Damage;

    public int PoisonDamage = 1;
    public int numOfPosionHits = 3;
    public bool isPoison = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Invoke("DestroyTime", 2f);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("PlayerTongue").GetComponent<TongueShot>().isPoison == true)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
            isPoison = true;
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
