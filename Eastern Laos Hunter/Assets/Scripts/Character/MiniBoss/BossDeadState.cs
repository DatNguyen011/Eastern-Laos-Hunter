using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : IState<Boss>
{
    public float time;
    public void OnEnter(Boss miniBoss)
    {
        miniBoss.ChangeAnime("Dead");
    }

    public void OnExecute(Boss miniBoss)
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            miniBoss.OnDead();
        }
    }

    public void OnExit(Boss miniBosst)
    {

    }
}
