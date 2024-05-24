using UnityEngine;
using System.Collections;

public class FlyPushState : State
{
    private Enemy _enemy;
    private Vector3 _target;
    private GameObject _bullet;

    public FlyPushState(Enemy enemy, GameObject bullet)
    {
        _enemy = enemy;
        _target = new Vector3(_enemy.Target.position.x, _enemy.Target.position.y + 5f, 0);
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

        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _target, _enemy.Speed * Time.deltaTime);

        if ((_enemy.transform.position - _target).magnitude < 2f)
        {
            _enemy.SM.ChangeState(new FlyAttackState(_enemy, _bullet));
        }
    }
}