﻿using UnityEngine;
using System.Collections;

public class EagleIdleState : State
{
    private Enemy _enemy;

    public EagleIdleState(Enemy enemy)
    {
        _enemy = enemy;
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
            _enemy.SM.ChangeState(new PushPointState(_enemy, _enemy.Target.position));
        }
    }
}