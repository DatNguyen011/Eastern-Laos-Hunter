using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float time = 0;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("Idle");
        //this.OnExecute(bot);
        //Debug.Log("idle state");
    }

    public void OnExecute(Bot bot)
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
            //Debug.Log(time);
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}