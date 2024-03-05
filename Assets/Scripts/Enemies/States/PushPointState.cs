using UnityEngine;

public class PushPointState : State
{
    private Enemy _enemy;
    private Vector3 _target;
    private Vector3 _direction;

    public PushPointState(Enemy enemy, Vector3 target)
    {
        _enemy = enemy;
        _target = target;
        _direction = target - enemy.transform.position;
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

        if ((_enemy.transform.position - _target).magnitude < 0.1f)
        {
            _enemy.SM.ChangeState(new FlyBehindState(_enemy, _direction));
        }
    }
}
