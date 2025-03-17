using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    float time = 0;
    public void OnEnter(Bot bot)
    {
        if(bot.attackArea!=null)
        {
            bot.ChangeAnim("Attack");
            bot.attackArea.SetActive(true);

        }
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (time > .5)
        {
            //Hero.Instance.ReduceHp(20f);
            bot.OnAttack();
            
        }

    }

    public void OnExit(Bot bot)
    {

    }

}
