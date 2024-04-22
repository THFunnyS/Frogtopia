using UnityEngine;
using System.Collections;

public class CaterpillarPushState : State
{
    private Enemy _enemy;
    private Vector3 _target;

    public CaterpillarPushState(Enemy enemy)
    {
        _enemy = enemy;
        _target = new Vector3(_enemy.Target.position.x, _enemy.Target.position.y + 7f, 0);
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

        _enemy.transform.position = new Vector3(Mathf.MoveTowards(_enemy.transform.position.x, _enemy.Target.position.x, _enemy.Speed * Time.deltaTime), _enemy.transform.position.y);
        
        if (Vector2.Distance(_enemy.transform.position, _enemy.Target.position) > _enemy.AgressDistance)
        {
            _enemy.SM.ChangeState(new CaterpillarIdleState(_enemy));
        }
    }
}
