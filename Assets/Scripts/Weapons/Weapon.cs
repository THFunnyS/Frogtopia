using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator anim;

    public GameObject weaponIcon;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetTrigger("AttackStart");
            
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetTrigger("ContinueAttack");
        }
    }
}
