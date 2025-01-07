using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PatrolState : IState<Bot>
{
    Vector2 pos;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Run");
        //pos = (Vector2)Random.insideUnitCircle * 3f;
        pos = bot.RandomPoint();
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isTouchWall == false)
        {
            if (Vector2.Distance(bot.transform.position, pos) < 0.01f)
            {
                bot.ChangeState(new IdleState());
            }
            else if (Vector2.Distance(Hero.Instance.transform.position, bot.transform.position) <= 5f)
            {
                bot.FindPlayer();

            }
            else if (Vector2.Distance(Hero.Instance.transform.position, bot.transform.position) > 5f)
            {
                bot.SetDestination(pos);
            }
        }
        else
        {
            bot.SetDestination(bot.startPoint);
            if(Vector2.Distance(bot.transform.position, bot.startPoint)<0.01f) {
                bot.isTouchWall=false;
                bot.ChangeState(new IdleState());
            }
        }

    }

    public void OnExit(Bot bot)
    {

    }
}
