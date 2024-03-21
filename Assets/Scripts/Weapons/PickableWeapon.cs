using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableWeapon : MonoBehaviour
{
    public Transform Player;
    public GameObject weapon;
    public GameObject weaponPlace;
    public GameObject pickUpWeaponButton;
    bool pickable = false;
    bool isArmed;
    public GameObject currentWeapon;
    GameObject currentWeaponChild;
    GameObject currentWeaponIcon;

    float t = 0;
    float Offset = 0;
    float Amp = 0.25f;
    float Freq = 2;
    Vector2 StartPos;

    public Image icon;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartPos = transform.position;
    }

    void Update()
    {
        t += Time.deltaTime;
        Offset = Amp * Mathf.Sin(t * Freq);
        transform.position = StartPos + new Vector2(0, Offset);
        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 1.8f)
        {
            pickable = true;
            pickUpWeaponButton.SetActive(true);
        }
        else
        {
            pickable = false;
            pickUpWeaponButton.SetActive(false);
        }
        isArmed = Player.GetComponent<PlayerMovements>().isArmed;
        PickUp();
    }

    void PickUp()
    {
        if (pickable == true && Input.GetKeyDown(KeyCode.E))
        {
            icon.GetComponent<Image>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
            Instantiate(weapon, weaponPlace.transform);
            currentWeaponChild = currentWeapon.transform.GetChild(0).gameObject;
            if (isArmed == true)
            {
                DropCurrentWeapon();
            }
            Player.GetComponent<PlayerMovements>().isArmed = true;
            Destroy(gameObject);
        }
    }

    void DropCurrentWeapon()
    {
        currentWeaponIcon = currentWeaponChild.GetComponent<Weapon>().weaponIcon;
        Instantiate(currentWeaponIcon, Player.position, Quaternion.identity);
        Destroy(currentWeaponChild);
    }
}
