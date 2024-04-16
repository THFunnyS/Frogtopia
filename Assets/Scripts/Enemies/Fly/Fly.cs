using UnityEngine;
using System.Collections;

public class Fly : Enemy
{
    [SerializeField] private GameObject _bullet;
    public AudioSource flySound;

    public override void Start()
    {
        base.Start();
        SM.Initialize(new FlyIdleState(this, _bullet));
        flySound.Play();
    }

    public override void Update()
    {
        base.Update();
    }
}
