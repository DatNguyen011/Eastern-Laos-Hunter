using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IdleState : IState<Bot>    
{
    float time = 0;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Idle");
        
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (bot.isDead == true)
        {
            bot.ChangeState(new DeadState());
        }
        else
        {
            if (time > 1f)
            {
                bot.ChangeState(new PatrolState());

            }
        }

    }

    

    public void OnExit(Bot bot)
    {
       
    }

}
