using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Hero")
        {
            collision.GetComponent<Hero>().ReduceHp(10f);
        }
    }
}
