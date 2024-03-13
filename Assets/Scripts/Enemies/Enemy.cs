using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int lives = 5;
    public float AgressDistance = 10f;
    public float Speed = 5f;
    public float RestTime = 2f;

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
        if (col.tag == "PlayerBullet" || col.tag == "Weapon")
        {
            lives--;
        }
    }
}
