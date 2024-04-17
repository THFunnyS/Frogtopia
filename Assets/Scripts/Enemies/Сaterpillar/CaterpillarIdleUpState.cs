using UnityEngine;
using System.Collections;

public class CaterpillarIdleUpState : State
{
    private Enemy _enemy;

    public CaterpillarIdleUpState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public override void Update()
    {
        base.Update();

        if (_enemy.transform.position.x - _enemy.Target.position.x < _enemy.AgressDistance)
        {
            _enemy.SM.ChangeState(new CaterpillarPushState(_enemy));
        }
    }
}
