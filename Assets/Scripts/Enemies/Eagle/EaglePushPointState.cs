using UnityEngine;

public class EaglePushPointState : State
{
    private Enemy _enemy;
    private Vector3 _target;
    private Vector3 _direction;

    public EaglePushPointState(Enemy enemy)
    {
        _enemy = enemy;
        _target = _enemy.Target.position;
        _direction = _target - enemy.transform.position;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.PlaySound(AudioManager.inst.EagleAttack);
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
            _enemy.SM.ChangeState(new EagleFlyBehindState(_enemy, _direction));
        }
    }
}
