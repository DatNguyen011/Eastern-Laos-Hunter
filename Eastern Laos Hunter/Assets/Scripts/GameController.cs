using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Bot bot = new Bot();
    // Start is called before the first frame update
    void Start()
    {
        bot.OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
