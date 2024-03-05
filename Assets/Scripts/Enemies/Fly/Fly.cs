using UnityEngine;
using System.Collections;

public class Fly : Enemy
{
    [SerializeField] private GameObject _bullet;

    public override void Start()
    {
        base.Start();

        StartCoroutine(CheckTarget());
    }

    public override void Update()
    {
        base.Update();
    }

    IEnumerator CheckTarget()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);

            if (Vector2.Distance(transform.position, Target.position) < AgressDistance)
            {
                GameObject bullet = Instantiate(_bullet, gameObject.transform);
                Destroy(bullet, 5f);
            }
        }
    }
}
