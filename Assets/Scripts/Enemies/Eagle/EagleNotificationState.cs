using UnityEngine;
using System.Collections;

public class EagleNotificationState : State
{
    private Enemy _enemy;
    private float _leftTime;

    public EagleNotificationState(Enemy enemy)
    {
        _enemy = enemy;
        _leftTime = _enemy.NotifTime;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.Notification.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.Notification.gameObject.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (_leftTime <= 0)
            _enemy.SM.ChangeState(new EaglePushPointState(_enemy));
        else
            _leftTime -= Time.deltaTime;
    }
}