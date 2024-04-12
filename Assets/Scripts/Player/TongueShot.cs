using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueShot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletTransform;

    public float StartTimeFire;
    private float TimeFire;

    public bool isPoison = false;
    void Start()
    {
        TimeFire = StartTimeFire;
    }

    void Update()
    {
        //if (isPoison) Bullet.GetComponent<PlayerBullet>().isPoison = true;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (TimeFire <= 0)
            {
                Instantiate(Bullet, BulletTransform.position, transform.rotation);
                TimeFire = StartTimeFire;
            }
            else
            {
                TimeFire -= Time.deltaTime;
            }
        }
    }
}
