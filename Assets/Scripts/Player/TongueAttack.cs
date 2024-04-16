using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttack : MonoBehaviour
{
    public Animator anim;
    public int Damage;
    public bool Knockback = false;
    public AudioClip attackSound;
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
        AudioSource.PlayClipAtPoint(attackSound, transform.position);
        yield return new WaitForSeconds(attackSound.length);
        isAttacking = false;
    }
}
