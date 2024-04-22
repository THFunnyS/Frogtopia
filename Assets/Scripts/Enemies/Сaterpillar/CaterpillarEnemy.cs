using UnityEngine;
using System.Collections;

public class CaterpillarEnemy : Enemy
{ 
    public override void Start()
    {
        base.Start();
        SM.Initialize(new CaterpillarIdleUpState(this));
    }

    public override void Update()
    {
        base.Update();
    }
}
