using UnityEngine;
using System.Collections;

public class RestState : State
{
    private Enemy _enemy;
    private float _leftTime;

    public RestState(Enemy enemy)
    {
        _enemy = enemy;
        _leftTime = _enemy.RestTime;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (_leftTime <= 0)
            _enemy.SM.ChangeState(new EagleIdleState(_enemy));
        else
            _leftTime -= Time.deltaTime;
    }
}
