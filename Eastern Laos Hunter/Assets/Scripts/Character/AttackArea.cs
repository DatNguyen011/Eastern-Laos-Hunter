using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Bot")
        {
            collision.GetComponent<Bot>().ReduceHp(30f);
        }
        if (collision.gameObject.tag=="Box")
        {
            collision.GetComponent<Box>().DestroyBox();
        }

    }

}
