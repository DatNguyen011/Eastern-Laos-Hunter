using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BossAttackState : IState<Boss>
{
    public float time = 0;
    public void OnEnter(Boss miniBoss)
    {
        miniBoss.ChangeAnime("Attack2");

        miniBoss.direction = Hero.Instance.transform.position - miniBoss.transform.position;
        miniBoss.direction = miniBoss.direction.normalized;
    }

    public void OnExecute(Boss miniBoss)
    {

            if (miniBoss.isTouchWall == false)
            {
                miniBoss.attackArea.SetActive(true);
                miniBoss.OnAttack();
            }
            else if (miniBoss.isTouchWall == true)
            {
                miniBoss.isTouchWall = false;
                miniBoss.attackArea.SetActive(false);
                miniBoss.ChangeState(new BossIdleState());

            }

    }

    public void OnExit(Boss miniBosst)
    {

    }
}
