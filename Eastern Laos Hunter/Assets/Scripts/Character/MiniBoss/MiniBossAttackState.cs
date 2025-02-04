using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MiniBossAttackState : IState<MiniBoss>
{
    public float time = 0;
    public void OnEnter(MiniBoss miniBoss)
    {
        miniBoss.ChangeAnime("Attack2");

        miniBoss.direction = Hero.Instance.transform.position - miniBoss.transform.position;
        miniBoss.direction = miniBoss.direction.normalized;
    }

    public void OnExecute(MiniBoss miniBoss)
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
                miniBoss.ChangeState(new MiniBossIdleState());

            }

    }

    public void OnExit(MiniBoss miniBosst)
    {

    }
}
