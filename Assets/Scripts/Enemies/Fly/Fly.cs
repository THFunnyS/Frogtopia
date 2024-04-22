using UnityEngine;
using System.Collections;

public class Fly : Enemy
{
    [SerializeField] private GameObject _bullet;

    public override void Start()
    {
        base.Start();
        SM.Initialize(new FlyIdleState(this, _bullet));
    }

    public override void Update()
    {
        base.Update();
    }
}
