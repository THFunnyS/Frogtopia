using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttack : MonoBehaviour
{
    public Animator anim;
    public int Damage;
    public bool Knockback = false;
    private bool isAttacking = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            anim.SetTrigger("attack");
            if (!isAttacking) StartCoroutine(PlayAttackSound());
        }
    }

    IEnumerator PlayAttackSound()
    {
        isAttacking = true;
        AudioManager.PlaySound(AudioManager.inst.TongueAttack);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
