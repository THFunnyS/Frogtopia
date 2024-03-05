using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float AgressDistance = 10f;
    public float Speed = 5f;
    public float RestTime = 2f;

    public Transform Target { get; set; }
    public StateMachine SM { get; set; }

    public virtual void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SM = new StateMachine();
    }

    public virtual void Update()
    {
        SM.CurrentState.Update();
    }
}
