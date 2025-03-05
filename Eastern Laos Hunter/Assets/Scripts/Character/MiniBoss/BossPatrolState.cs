using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrolState : IState<Boss>
{
    public float time = 0;
    public void OnEnter(Boss miniBoss)
    {
        miniBoss.ChangeAnime("Attack1");
        miniBoss.ThrowBullet();

    }

    public void OnExecute(Boss miniBoss)
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            miniBoss.ChangeState(new BossAttackState());   
        }
    }

    public void OnExit(Boss miniBosst)
    {

    }
}
