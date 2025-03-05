using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBot : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            GameController.Instance.OnInit();
            Destroy(gameObject);
        }
    }
}
