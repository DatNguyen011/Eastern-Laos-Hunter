using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.tag);
        if (collision.gameObject.tag=="Bot")
        {
            Debug.Log("dame");
            collision.GetComponent<Bot>().ReduceHp(20f);
        }
    }

}
