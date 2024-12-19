using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PatrolState : IState<Bot>
{
    Vector3 pos;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Run");
        pos = (Vector3)Random.insideUnitCircle * 3f;
    }

    public void OnExecute(Bot bot)
    {
        
        if (Vector2.Distance(bot.transform.position, pos) < 0.01f)
        {
            bot.ChangeState(new IdleState());
        }
        else if(Vector2.Distance(Hero.Instance.transform.position, bot.transform.position)<=5f)
        {
            bot.FindPlayer();
            
        }
        else
        {
            bot.SetDestination(pos);
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
