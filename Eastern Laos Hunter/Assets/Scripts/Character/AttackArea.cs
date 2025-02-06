using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Bot")
        {
            collision.GetComponent<Bot>().ReduceHp(30f);
        }
        if (collision.tag=="Box")
        {
            collision.GetComponent<Box>().DestroyBox();
        }
        if (collision.tag == "MiniBoss")
        {
            collision.GetComponent<MiniBoss>().ReduceHp(20f);    
        }

    }

}
