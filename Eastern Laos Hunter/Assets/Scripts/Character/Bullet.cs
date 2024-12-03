using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private void Update()
    {
        transform.Rotate(0,0,360f*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bot")
        {
            collision.GetComponent<Bot>().ReduceHp(30f);
        }
    }
}
