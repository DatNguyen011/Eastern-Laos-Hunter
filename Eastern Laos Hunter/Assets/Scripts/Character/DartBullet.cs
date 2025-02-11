using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            Hero.Instance.ReduceHp(10f);
            Destroy(gameObject);
        }
    }
}
