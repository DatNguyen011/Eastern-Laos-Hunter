using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    Vector2 pos;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Run");
        bot.botVFX.SetActive(true);
        bot.SpawnVfx();
        pos = bot.RandomPoint();
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isTouchWall == false)
        {
            if (bot.typeBot == "longrange"&& Vector2.Distance(bot.transform.position, Hero.Instance.transform.position) < 7f&&bot.isThrow==false)
            {
                bot.ChangeState(new AttackState());
            }
            else
            {
                if (Vector2.Distance(bot.transform.position, pos) < 0.01f)
                {
                    bot.ChangeState(new IdleState());
                }
                else if (Vector2.Distance(Hero.Instance.transform.position, bot.transform.position) <= 5f)
                {
                    bot.botVFX.SetActive(false);
                    bot.FindPlayer();
                }
                else if (Vector2.Distance(Hero.Instance.transform.position, bot.transform.position) > 5f)
                {
                    bot.SetDestination(pos);
                }
            }

        }
        else
        {
            bot.SetDestination(bot.startPoint);
            if (Vector2.Distance(bot.transform.position, bot.startPoint) < 0.01f)
            {
                bot.isTouchWall = false;
                bot.ChangeState(new IdleState());
            }
        }

    }

    public void OnExit(Bot bot)
    {

    }
}
