using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public GameObject ArmorSkin;
    public GameObject PoisonCloud;
    public GameObject ElectroWave;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Sprite GetSkillSprite(string name)
    {
        switch (name)
        {
            case "PoisonCloud":
                return PoisonCloud.GetComponent<SpriteRenderer>().sprite;
            case "ArmorSkin":
                return ArmorSkin.GetComponent<SpriteRenderer>().sprite;
            case "ElectroWave":
                return ElectroWave.GetComponent<SpriteRenderer>().sprite;
            default: return gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
