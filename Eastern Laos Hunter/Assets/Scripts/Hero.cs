using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Singleton<Hero>
{
    
    [SerializeField] float speed=5f;
    private bool facingRight=true;
    public Animator animator;
    private string currentAnim;
    private bool isAttack=false;
    public float timeToDash = 5f;
    public float dashTime = 1f;
    
    void Start()
    {
        
    }

    //Joystick
    //void Update()
    //{       
    //    transform.position += JoystickControl.direct*speed*Time.deltaTime;
        
    //    if (JoystickControl.direct.x>0&& !facingRight)
    //    {
    //        facingRight = !facingRight;
    //        transform.Rotate(0, 180f, 0);
    //        ChangeAnim("Run");
            
    //    }
    //    else if (JoystickControl.direct.x < 0&&facingRight)
    //    {
    //        facingRight = !facingRight;
    //        transform.Rotate(0, 180f, 0);
    //        ChangeAnim("Run");
            
    //    }
    //    else if (JoystickControl.direct == Vector3.zero && !isAttack)
    //    {
    //        ChangeAnim("Idle");
    //    }
    //}

    //AWDS
    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 move=new Vector3 (moveX, moveY, 0)*speed*Time.deltaTime;
        transform.position += move;

        if (move.x > 0 || move.y > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180f, 0);
            ChangeAnim("Run");

        }
        else if (move.x < 0 || move.y < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180f, 0);
            ChangeAnim("Run");

        }
        else if (move == Vector3.zero && !isAttack)
        {
            ChangeAnim("Idle");
        }
    }

    public void Attack()
    {
        ChangeAnim("Attack");
        isAttack = true;
        StartCoroutine(ReturnIdle());
    }

    IEnumerator ReturnIdle()
    {
        yield return new WaitForSeconds(.5f);
        isAttack=false;
    }

    public void Dash()
    {
        speed = speed+5f;
        StartCoroutine(CountdownDashTime());
    }

    IEnumerator CountdownDashTime()
    {
        yield return new WaitForSeconds(dashTime);
        speed = 5f;
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
