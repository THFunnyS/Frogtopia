using UnityEngine;

public class IdleState : State
{
    private Enemy _enemy;
    private State _state;

    public IdleState(Enemy enemy, State state)
    {
        _enemy = enemy;
        _state = state;
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
            _enemy.SM.ChangeState(_state);
        }
    }

}
