using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    Vector3 pos;
    Camera cam;
    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        Flip();
        pos = cam.WorldToScreenPoint(transform.position);
    }

    void Flip()
    {
        if (Input.mousePosition.x < pos.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.mousePosition.x > pos.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}