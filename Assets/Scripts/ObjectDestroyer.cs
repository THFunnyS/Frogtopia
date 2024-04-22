using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
