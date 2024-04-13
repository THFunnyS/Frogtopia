using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttack : MonoBehaviour
{
    public Animator anim;
    public int Damage;
    public bool Knockback = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            anim.SetTrigger("attack");
        }
    }
}
