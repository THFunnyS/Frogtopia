using UnityEngine;

public class Eagle : Enemy
{
    public override void Start()
    {
        base.Start();

        SM.Initialize(new EagleIdleState(this));
    }
    
    public override void Update()
    {
        base.Update();
    }
}
