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

    public Image icon;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
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
        PickUp();
    }

    void PickUp()
    {
        if (pickable == true && Input.GetKeyDown(KeyCode.E))
        {
            icon.GetComponent<Image>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
            Instantiate(weapon, weaponPlace.transform);
            Destroy(gameObject);
        }
    }
}
