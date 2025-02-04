using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossIdleState : IState<MiniBoss>
{
    public float time=0;
    public void OnEnter(MiniBoss miniBoss)
    {
        miniBoss.ChangeAnime("Idle");
        
    }

    public void OnExecute(MiniBoss miniBoss)
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
    
            miniBoss.ChangeState(new MiniBossPatrolState());
        }
    }

    public void OnExit(MiniBoss miniBosst)
    {
        
    }
}
