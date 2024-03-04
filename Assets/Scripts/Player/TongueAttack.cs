using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAttack : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            anim.SetTrigger("attack");
        }
        else anim.SetTrigger("stop");
        //if (!Input.GetKey(KeyCode.Mouse1))
        //{
        //    anim.SetTrigger("stop");
        //}
    }
}
