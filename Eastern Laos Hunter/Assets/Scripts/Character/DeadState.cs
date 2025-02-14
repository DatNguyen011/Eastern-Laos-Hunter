using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState<Bot>
{
    float time = 0;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim("Dead");
    }

    public void OnExecute(Bot t)
    {
        time += Time.deltaTime;
        if (time > .6)
        {
            t.isDead = false;
            
        }
    }

    public void OnExit(Bot t)
    {
        
    }
}
