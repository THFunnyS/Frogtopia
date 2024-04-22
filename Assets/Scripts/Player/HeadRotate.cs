using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    public float offset;

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);


        Vector3 LocalScale = Vector3.one;
        if (rotateZ > 90 || rotateZ < -90)
        {
            LocalScale.x = -1f;
            LocalScale.y = -1f;
        }
        else
        {
            LocalScale.x = +1f;
            LocalScale.y = +1f;
        }
        transform.localScale = LocalScale;
    }
}