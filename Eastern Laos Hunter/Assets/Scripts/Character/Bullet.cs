using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bot"|| collision.gameObject.tag == "MiniBot")
        {
            collision.GetComponent<Bot>().ReduceHp(30f);
            Destroy(gameObject);
        }
    }
}
