using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenBot : Singleton<OpenBot>
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            GameController.Instance.OnInit(SceneManager.GetActiveScene().buildIndex);
            Destroy(gameObject);
        }
    }
}
