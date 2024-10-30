using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Singleton<Hero>
{

    [SerializeField] float speed = 5f;
    private bool facingRight = true;
    public Animator animator;
    private string currentAnim;
    private bool isAttack = false;
    public float countDownDash = 5f;
    public float dashTime = 0.3f;
    public CounterTime couterTime=new CounterTime();
    public GameObject attackArea;
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
        if (isAttack == false)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(moveX, moveY, 0).normalized;
            transform.position += move * speed * Time.deltaTime;
            if(move == Vector3.zero)
            {
                ChangeAnim("Idle");
            }
            
            if (move != Vector3.zero)
            {
                ChangeAnim("Run");
                if (move.x > 0 && !facingRight)
                {
                    facingRight = !facingRight;
                    transform.Rotate(0, 180f, 0);
                }
                else if (move.x < 0 && facingRight)
                {
                    facingRight = !facingRight;
                    transform.Rotate(0, 180f, 0);
                }
            }
            if(Input.GetKeyDown(KeyCode.J))
            {
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                Dash();
            }

        }

    }

    public void Attack()
    {
        if (isAttack == false)
        {
            attackArea.SetActive(true);
            ChangeAnim("Attack");
            isAttack = true;
            StartCoroutine(ReturnIdle());
        }

    }

    IEnumerator ReturnIdle()
    {
        yield return new WaitForSeconds(.5f);
        isAttack = false;
        attackArea.SetActive(false);
    }

    public void Dash()
    {
        speed = speed + 5f;
        UIManager.Instance.btnFlash.interactable = false;
        StartCoroutine(Dashing());
        StartCoroutine(CountdownDashTime());
    }

    IEnumerator Dashing()
    {
        yield return new WaitForSeconds(dashTime);
        speed = 5f;
    }

    IEnumerator CountdownDashTime()
    {
        yield return new WaitForSeconds(countDownDash);
        UIManager.Instance.btnFlash.interactable = true;
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
