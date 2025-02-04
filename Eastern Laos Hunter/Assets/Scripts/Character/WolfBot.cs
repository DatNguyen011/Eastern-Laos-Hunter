using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBot : Bot
{
    void Start()
    {
        OnInit();
    }

    void LateUpdate()
    {
        UpdateState();
    }

    public override void OnInit()
    {
        healthBar.SetHealth(maxHp, hp);
        base.OnInit();
    }

    public override void OnDead()
    {
        base.OnDead();
    }

    public override void OnAttack()
    {

        base.OnAttack();
    }
}
