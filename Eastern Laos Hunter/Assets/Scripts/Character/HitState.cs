using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : IState<Bot>
{
    float time=0;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Hit");
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (time > 0.3f)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }

}
