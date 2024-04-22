using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyBullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Transform _target;
    public float Damage;
    public float KnockbackPower;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        float distance = Mathf.Abs(transform.position.x - _target.position.x);
        float hight = Mathf.Abs(transform.position.y - _target.position.y);
        float Side = transform.position.x - _target.position.x <= 0 ? 1 : -1;

        float StartVelocityX = Mathf.Sqrt(-0.5f * Physics2D.gravity.y * Mathf.Pow(distance, 2) / hight);
        StartVelocityX *= Side;

        _rb.velocity = new Vector2(StartVelocityX, 0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
