using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoBot : Bot
{
    //public GameObject geckoCollider;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        
        //Moving();
    }

    // Update is called once per frame
    void Update()
    {
       
        //FindPlayer();
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
    IEnumerator AttackCountDown()
    {
        yield return new WaitForSeconds(1f);
        //geckoCollider.SetActive(false);
        ChangeAnim("Idle");
    }
}
