using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : IState<Boss>
{
    public float time=0;
    public void OnEnter(Boss miniBoss)
    {
        miniBoss.ChangeAnime("Idle");
        
    }

    public void OnExecute(Boss miniBoss)
    {
        time += Time.deltaTime;
        miniBoss.Direction();    
        if (time > 3f)
        {
            miniBoss.ChangeState(new BossPatrolState());
        }
    }

    public void OnExit(Boss miniBosst)
    {
        
    }
}
