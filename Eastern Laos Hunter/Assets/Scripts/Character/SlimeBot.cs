using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBot : Bot
{
    public GameObject slimePrefab; 
    public Transform dropPoint;
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
        maxHp = 120f;
        hp = 120f;
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

    void SpawnSlime()
    {
        if (slimePrefab != null)
        {
            MyPoolManager.InstancePool.Get(slimePrefab, dropPoint.position);
            slimePrefab.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            Hero.Instance.ReduceHp(2f);
            SpawnSlime();
        }
    }
}
