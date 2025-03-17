using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigNinja : Bot
{
    
    void Start()
    {
       
        OnInit();
    }

    void Update()
    {
        UpdateState();
    }

    public override void OnInit()
    {
        maxHp = 200f;
        hp = 200f;
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
