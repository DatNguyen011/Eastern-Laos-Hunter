using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBot : Bot
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        healthBar.SetHealth(100f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthBar.SetHealth(100f, 10f);
        }
    }

    public override void OnInit()
    {
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
