﻿using UnityEngine;
using System.Collections;

public class FlyAttackState : State
{
    private Enemy _enemy;
    float startTime;
    GameObject _bullet;

    public FlyAttackState(Enemy enemy, GameObject bullet)
    {
        _enemy = enemy;
        _bullet = bullet;
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - startTime > 3f)
        {
            if (Vector2.Distance(_enemy.transform.position, _enemy.Target.position) < 1.7f * _enemy.AgressDistance)
            {
                GameObject bullet = Object.Instantiate(_bullet, _enemy.transform);
                Object.Destroy(bullet, 5f);
            }
            startTime = Time.time;
        }

        if (Vector2.Distance(_enemy.transform.position, _enemy.Target.position) > 1.7f * _enemy.AgressDistance)
        {
            _enemy.SM.ChangeState(new FlyIdleState(_enemy, _bullet));
        }
    }
}