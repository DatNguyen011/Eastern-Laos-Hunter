using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Singleton<Hero>
{
    
    [SerializeField] float speed=5f;
    private bool facingRight=true;
    public Animator animator;
    private string currentAnim;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
        transform.position += JoystickControl.direct*speed*Time.deltaTime;
        if (JoystickControl.direct.x>0&& !facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180f, 0);
            ChangeAnim("Run");
        }else if (JoystickControl.direct.x < 0&&facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180f, 0);
            ChangeAnim("Run");
        }
        else if (JoystickControl.direct == Vector3.zero)
        {
            ChangeAnim("Idle");
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
