using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int lives;
    private int TakenDamage;
    public float AgressDistance = 10f;
    public float Speed = 5f;
    public float RestTime = 2f;

    private Transform currentWeapon;
    public Transform Target { get; set; }
    public StateMachine SM { get; set; }

    public virtual void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SM = new StateMachine();
    }

    public virtual void Update()
    {
        if(SM.CurrentState != null)
            SM.CurrentState.Update();

        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "PlayerBullet")
        {
            TakenDamage = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().Damage;
            lives -= TakenDamage;
        }

        if (col.tag == "PlayerTongue")
        {
            TakenDamage = GameObject.FindGameObjectWithTag("PlayerTongue").GetComponent<TongueAttack>().Damage;
            lives -= TakenDamage;
        }

        if (col.tag == "Weapon")
        {
            currentWeapon = GameObject.FindGameObjectWithTag("PlayerWeaponPlace").GetComponent<Transform>();
            TakenDamage = currentWeapon.transform.GetChild(0).gameObject.GetComponent<Weapon>().Damage;
            lives -= TakenDamage;
        }
    }
}
