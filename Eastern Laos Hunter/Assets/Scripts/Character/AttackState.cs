using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    float time = 0;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Attack");

    }

    public void OnExecute(Bot bot)
    {
        //        Debug.Log("Attack State");
        time += Time.deltaTime;
        if (time > .5)
        {
            bot.ChangeState(new IdleState());
            Hero.Instance.ReduceHp(10f);
            bot.haveTarget = false;
        }

    }

    public void OnExit(Bot bot)
    {

    }

}
