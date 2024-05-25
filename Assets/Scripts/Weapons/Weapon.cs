using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator anim;

    public GameObject weaponIcon;

    public int Damage;
    
    private bool isAttacking = false;


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetTrigger("AttackStart");
            if (!isAttacking) StartCoroutine(PlayAttackSound());

        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetTrigger("ContinueAttack");
        }
    }

    IEnumerator PlayAttackSound()
    {
        isAttacking = true;
        AudioManager.PlaySound(AudioManager.inst.WeaponAttack);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
