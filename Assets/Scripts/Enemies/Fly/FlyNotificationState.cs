using UnityEngine;
using System.Collections;

public class FlyNotificationState : State
{
    private Enemy _enemy;
    private GameObject _bullet;
    private float _leftTime;

    public FlyNotificationState(Enemy enemy, GameObject bullet)
    {
        _enemy = enemy;
        _bullet = bullet;
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
            _enemy.SM.ChangeState(new FlyPushState(_enemy, _bullet));
        else
            _leftTime -= Time.deltaTime;
    }
}
