using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CounterTime
{
    UnityAction playerAction;
    float time;
    public void OnStart(UnityAction playerAction, float time)
    {
        this.playerAction = playerAction;
        this.time = time;
    }

    public void OnExecute()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                OnExit();
            }
        }
    }

    public void OnExit()
    {
        playerAction?.Invoke();
    }

    public void OnCancel()
    {
        playerAction = null;
    }
}
