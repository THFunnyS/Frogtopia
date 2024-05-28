using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationLevel1 : MonoBehaviour
{
    private int roomSize = 40;
    private string[] rooms = { "Rooms/Level1/2 (2)/Room_2_1", "Rooms/Level1/2 (2)/Room_2_2" };
    private string[] corridors = { "Rooms/Level1/corridor1", "Rooms/Level1/corridor2" };
    private string[] underground = { "Rooms/Level1/underground1"};
    
    void placeRoom(string name, int x, int y)
    {
        GameObject prefab = Resources.Load<GameObject>(name);
        Instantiate(prefab, transform).transform.position = new Vector3 (x * roomSize, y * roomSize, 0);
    }
    void placeRandomRoom(string[] rooms, int x, int y)
    {
        int idx = Random.Range(0, corridors.Length);
        GameObject prefab = Resources.Load<GameObject>(rooms[idx]);
        Instantiate(prefab, transform).transform.position = new Vector3(x * roomSize, y * roomSize, 0);
    }
    void Start()
    {
        placeRoom("Rooms/Level1/2 (2)/AutoLayer", 0, 0);
        placeRandomRoom(rooms, 1, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
