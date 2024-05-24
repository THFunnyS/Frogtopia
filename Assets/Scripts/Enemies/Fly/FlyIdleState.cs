using UnityEngine;
using System.Collections;

public class FlyIdleState : State
{
    private Enemy _enemy;
    private GameObject _bullet;

    public FlyIdleState(Enemy enemy, GameObject bullet)
    {
        _enemy = enemy;
        _bullet = bullet;
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

        if (Vector2.Distance(_enemy.transform.position, _enemy.Target.position) < _enemy.AgressDistance)
        {
            _enemy.SM.ChangeState(new FlyNotificationState(_enemy, _bullet));
        }
    }
}