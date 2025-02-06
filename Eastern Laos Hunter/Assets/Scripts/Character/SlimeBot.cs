using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBot : Bot
{
    
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        
        //Moving();
    }


    // Update is called once per frame
    void Update()
    {
        
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
