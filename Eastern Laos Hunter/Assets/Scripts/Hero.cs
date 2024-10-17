using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    
    [SerializeField] float speed=5f;
    private bool facingRight=true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
        transform.position += JoystickControl.direct*speed*Time.deltaTime;
        if (JoystickControl.direct.x>0&&!facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180f, 0);
        }else if (JoystickControl.direct.x < 0&&facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180f, 0);
        }
    }
}
