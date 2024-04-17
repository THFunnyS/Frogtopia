using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttack : MonoBehaviour
{
    public Animator anim;
    public float Damage;
    public bool Knockback = false;

    public bool isPoison = false;
    public float PoisonDamage = 1;
    public int numOfPosionHits = 3;

    public bool isVampirism = false;
    public float percentOfVamp = 0.1f;
    
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
}
