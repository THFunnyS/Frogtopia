using UnityEngine;

public class Eagle : Enemy
{
    public int lives;
    public override void Start()
    {
        base.Start();

        SM.Initialize(new EagleIdleState(this));
    }
    
    public override void Update()
    {
        base.Update();
        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerBullet")
        {
            lives--;
        }
    }
}
