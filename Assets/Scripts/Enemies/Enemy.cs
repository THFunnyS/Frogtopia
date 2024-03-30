using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int lives;
    private int TakenDamage;
    public int DealtDamage;
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
        switch (col.tag)
        {
            case "PlayerBullet":
                TakenDamage = GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBullet>().Damage;
                lives -= TakenDamage;
                break;
            case "PlayerTongue":
                TakenDamage = GameObject.FindGameObjectWithTag("PlayerTongue").GetComponent<TongueAttack>().Damage;
                lives -= TakenDamage;
                break;
            case "Weapon":
                currentWeapon = GameObject.Find("Weapon").GetComponent<Transform>();
                TakenDamage = currentWeapon.transform.GetChild(0).gameObject.GetComponent<Weapon>().Damage;
                lives -= TakenDamage;
                break;
        }
    }
}
