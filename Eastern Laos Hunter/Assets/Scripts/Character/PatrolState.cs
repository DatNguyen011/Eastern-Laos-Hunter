using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Run");
    }

    public void OnExecute(Bot bot)
    {
        
    }

    public void OnExit(Bot bot)
    {
        
    }
}
