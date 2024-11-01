using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Bot bot)
    {
        
    }

    public void OnExecute(Bot bot)
    {
        bot.Moving();
    }

    public void OnExit(Bot bot)
    {
        
    }
}
