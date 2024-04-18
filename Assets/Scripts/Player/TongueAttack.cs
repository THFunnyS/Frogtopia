using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttack : MonoBehaviour
{
    private GameObject player;

    public Animator anim;
    public float Damage;
    public bool Knockback = false;

    public bool isPoison = false;
    public float PoisonDamage = 1;
    public int numOfPosionHits = 3;

    public bool isVampirism = false;
    public float percentOfVamp = 0.1f;

    public bool PlayerKnockback = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            anim.SetTrigger("attack");
        }

        if (isPoison)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "Enemy" || col.gameObject.layer == 6) && PlayerKnockback)
        {
            StartCoroutine(PushedAway(col.gameObject.transform, 50));
        }
    }

    private IEnumerator PushedAway(Transform pushFrom, float pushPower) //откидывание от поверхностей и врагов
    {
        float time = 0;
        while (0.1 > time)
        {
            time += Time.deltaTime;
            Vector2 direction = (pushFrom.transform.position - player.transform.position).normalized;
            player.GetComponent<Rigidbody2D>().AddForce(-direction * pushPower);
        }
        yield return 0;
    }
}
