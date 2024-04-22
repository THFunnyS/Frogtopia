using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueShot : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletTransform;

    private Vector3 rotation = new Vector3(0, 0, 10);

    public float StartTimeFire;
    private float TimeFire;

    public bool isPoison = false;
    public bool isBurst = true;
    void Start()
    {
        TimeFire = StartTimeFire;
       
    }

    void Update()
    {
        Quaternion LocalRot = transform.rotation;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (TimeFire <= 0)
            {
                Instantiate(Bullet, BulletTransform.position, transform.rotation);    
                if (isBurst)
                {
                    transform.eulerAngles = rotation;
                     Debug.Log("1f"); Debug.Log(transform.rotation);
                    Instantiate(Bullet, BulletTransform.position, transform.rotation /*Quaternion.Euler(0,0,rot.z)*/);
                    transform.eulerAngles = -2*rotation;
                    Debug.Log("2f"); Debug.Log(transform.rotation);
                    Instantiate(Bullet, BulletTransform.position, transform.rotation /*Quaternion.Euler(0, 0, rot.z)*/);
                    transform.rotation = LocalRot;
                }
                TimeFire = StartTimeFire;
            }
            else
            {
                TimeFire -= Time.deltaTime;
            }
        }
    }
}
