using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossPatrolState : IState<MiniBoss>
{
    public float time = 0;
    public void OnEnter(MiniBoss miniBoss)
    {
        miniBoss.ChangeAnime("Attack1");
    }

    public void OnExecute(MiniBoss miniBoss)
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            miniBoss.ChangeState(new MiniBossAttackState());   
        }
    }

    public void OnExit(MiniBoss miniBosst)
    {

    }
}
