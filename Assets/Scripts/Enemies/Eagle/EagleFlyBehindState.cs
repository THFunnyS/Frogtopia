using UnityEngine;
using System.Collections;
using System;

public class EagleFlyBehindState : State
{
    private Enemy _enemy;
    private Vector3 _target;

    public EagleFlyBehindState(Enemy enemy, Vector3 direction)
    {
        _enemy = enemy;

        Vector3 upDirection = new Vector3(direction.x, Math.Abs(direction.y));
        _target = enemy.transform.position + new Vector3(upDirection.x, upDirection.y);
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.animator.SetBool("FlyingUp", true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _target, _enemy.Speed * Time.deltaTime);

        if ((_enemy.transform.position - _target).magnitude < 0.1f)
        {
            _enemy.animator.SetBool("FlyingUp", false);
            _enemy.SM.ChangeState(new EagleIdleState(_enemy));
        }
    }
}