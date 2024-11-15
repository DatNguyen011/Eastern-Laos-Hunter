using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Hit");
    }

    public void OnExecute(Bot bot)
    {
        
    }

    public void OnExit(Bot bot)
    {
        
    }

}
