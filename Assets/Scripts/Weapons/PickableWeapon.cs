using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickableWeapon : MonoBehaviour
{
    public Transform Player;
    public GameObject weapon; //префаб оружия
    public Transform weaponPlace; //где оружие будет появляться
    public GameObject pickUpWeaponButton; 
    bool pickable = false;
    bool isArmed;
    public Transform currentWeapon;
    GameObject currentWeaponChild; //оружие которое в руках
    GameObject currentWeaponIcon;

    public GameObject weaponInfoPanel;  //всегда активные родитель панели
    public GameObject weaponInfoPanelC; //панель, которая не активна

    float t = 0;         //для анимации 
    float Offset = 0;
    float Amp = 0.25f;
    float Freq = 2;
    Vector2 StartPos;

    public Image icon;
    void Start()
    {
        weaponPlace = GameObject.Find("Weapon").GetComponent<Transform>();
        currentWeapon = GameObject.Find("Weapon").GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartPos = transform.position;

        weaponInfoPanel = GameObject.Find("WeaponInfo");  
        weaponInfoPanelC = weaponInfoPanel.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        t += Time.deltaTime;
        Offset = Amp * Mathf.Sin(t * Freq);
        transform.position = StartPos + new Vector2(0, Offset);

        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 1.8f && Mathf.Abs(transform.position.y - Player.transform.position.y) < 1.8f)
        {
            pickable = true;
            pickUpWeaponButton.SetActive(true);
            WeaponInfoPanel();
            weaponInfoPanelC.SetActive(true);
        }
        else
        {
            pickable = false;
            pickUpWeaponButton.SetActive(false);
            weaponInfoPanelC.SetActive(false);
        }
        isArmed = Player.GetComponent<PlayerMovements>().isArmed; //проверка на наличие оружия
        PickUp();
    }

    void PickUp()
    {
        if (pickable && Input.GetKeyDown(KeyCode.E))
        {
            icon.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            Instantiate(weapon, weaponPlace.transform);
            currentWeaponChild = currentWeapon.transform.GetChild(0).gameObject;
            if (isArmed)
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
        GameObject droppedWeapon = Instantiate(currentWeaponIcon, Player.transform.position, Quaternion.identity);
        droppedWeapon.GetComponent<PickableWeapon>().icon = GameObject.Find("WeaponIcon").GetComponent<Image>();
        Destroy(currentWeaponChild);
    }

    void WeaponInfoPanel()
    {
        //заменить в панели картинку осматриваемого оружия
        weaponInfoPanelC.transform.GetChild(0).GetComponent<Image>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        //заменить в панели урон осматриваемого оружия
        weaponInfoPanelC.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = weapon.GetComponent<Weapon>().Damage.ToString();
    }
}
