using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bot")
        {
            collision.GetComponent<Bot>().ReduceHp(30f);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "MiniBoss")
        {
            collision.GetComponent<Boss>().ReduceHp(30f);
            Destroy(gameObject);
        }
    }
}
