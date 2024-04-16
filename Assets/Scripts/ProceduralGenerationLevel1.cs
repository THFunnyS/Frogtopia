using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationLevel1 : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefab, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
