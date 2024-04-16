using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationLevel1 : MonoBehaviour
{
    private int roomSize = 40;
    private string[] corridors = { "Rooms/Level1/corridor1", "Rooms/Level1/corridor2" };
    private string[] underground = { "Rooms/Level1/underground1"};

    public GameObject prefab;
    
    void Start()
    {
        prefab = Resources.Load<GameObject>("Rooms/Level1/startRoom");
        Instantiate(prefab, transform);
        int idx = Random.Range(0, corridors.Length);
        prefab = Resources.Load<GameObject>(corridors[idx]);
        Instantiate(prefab, transform).transform.position += new Vector3(40, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
