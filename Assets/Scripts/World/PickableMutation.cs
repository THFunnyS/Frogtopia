using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Effects
{
    /*BODY  */  DJUMP, ArmorSkin, PoisonCloud, ElectroWave, ProtectiveSlime, SharpTongue, ThickSkin,
    /*TONGUE*/ Knockback, PoisonShot , PoisonHit, Vampirism, PlayerKnockback
}

public class PickableMutation : MonoBehaviour
{
    public Transform Player;
    public Effects effects;

    public GameObject pickUpMutationButton;
    bool pickable = false;

    float t = 0;
    float Offset = 0;
    float Amp = 0.25f;
    float Freq = 2;
    Vector2 StartPos;

    private GameObject Tongue;
    private GameObject player;

    public GameObject Canvas;
    GameObject SkillSwaper;

    void Start()
    {  
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartPos = transform.position;
        Tongue = GameObject.FindGameObjectWithTag("PlayerTongue");
        player = GameObject.FindGameObjectWithTag("Player");
        SkillSwaper = Canvas.transform.GetChild(8).gameObject;
    }

    void Update()
    {
        t += Time.deltaTime;
        Offset = Amp * Mathf.Sin(t * Freq);
        transform.position = StartPos + new Vector2(0, Offset);

        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 1.8f && Mathf.Abs(transform.position.y - Player.transform.position.y) < 1.8f)
        {
            pickable = true;
            pickUpMutationButton.SetActive(true);
        }
        else
        {
            pickable = false;
            pickUpMutationButton.SetActive(false);
        }
        PickUp();
    }

    void PickUp()
    {
        if (pickable && Input.GetKeyDown(KeyCode.E))
        {
            switch (effects)
            {
                case Effects.DJUMP:
                    player.GetComponent<PlayerMovements>().canDoubleJump = true; 
                    break;
                case Effects.Knockback:
                    Tongue.GetComponent<TongueAttack>().Knockback = true;
                    break;
                case Effects.PoisonShot:
                    Tongue.GetComponent<TongueShot>().isPoison = true;
                    break;
                case Effects.PoisonHit:
                    Tongue.GetComponent<TongueAttack>().isPoison = true;
                    break;
                case Effects.Vampirism:
                    Tongue.GetComponent<TongueAttack>().isVampirism = true;
                    break;
                case Effects.ArmorSkin:
                    SkillSwaper.GetComponent<SkillSwaper>().AddToList("ArmorSkin");
                    break;
                case Effects.PlayerKnockback:
                    Tongue.GetComponent<TongueAttack>().PlayerKnockback = true;
                    break;
                case Effects.PoisonCloud:
                    SkillSwaper.GetComponent<SkillSwaper>().AddToList("PoisonCloud");
                    break;
                case Effects.ElectroWave:
                    SkillSwaper.GetComponent<SkillSwaper>().AddToList("ElectroWave");
                    break;
                case Effects.ProtectiveSlime:
                    player.GetComponent<PlayerMovements>().IncArmor();
                    break;
                case Effects.SharpTongue:
                    Tongue.GetComponent<TongueAttack>().Damage += 0.3f;
                    break;
                case Effects.ThickSkin:
                    player.GetComponent<PlayerMovements>().IncHealth();
                    break;
                
            }
            Destroy(gameObject);
        }
    }
}
