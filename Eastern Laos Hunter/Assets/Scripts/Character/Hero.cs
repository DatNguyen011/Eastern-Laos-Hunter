using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Singleton<Hero>
{

    [SerializeField] float speed = 5f;
    private bool facingRight = true;
    public Animator animator;
    private string currentAnim;
    private bool isAttack = false;
    private bool isDead = false;
    public float countDownDash = 5f;
    public float dashTime = 0.3f;
    public CounterTime couterTime = new CounterTime();
    public GameObject attackArea;
    public float maxHp = 100f;
    public float hp = 100f;
    public HealthBar healthBar;
    //public GameObject checkPoint;
    private Vector3 savePoint;
    public GameObject bullet;
    public GameObject arrow;
    public Transform bullerPos;
    public bool isHolding=false;    //public List<Vector2> checkPoints = new List<Vector2>();
    private GameObject newBullet;
    private GameObject oldBullet;
    public Transform pointRotation;
    private float angle;
    private float totalAngle;
    //private float maxAngle=60f;
    private float directionRotation=1f;
    //public HealthBar healthBar;
    void Start()
    {
        
        OnInit();
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
        if (isAttack==false&&isDead==false)
        {
            
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(moveX, moveY, 0).normalized;
            transform.position += move * speed * Time.deltaTime;

                 
            if (move == Vector3.zero)
            {
                ChangeAnim("Idle");
            }

            if (move != Vector3.zero)
            {
                couterTime.OnCancel();
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
            
            if (Input.GetKeyDown(KeyCode.J) && isAttack == false)
            {
                couterTime.OnExecute();
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {

                couterTime.OnCancel();
                Dash();
            }
            else if(Input.GetKeyDown(KeyCode.H))
            {
                isHolding = true;
                
                InitBullet();

            }
            else if (isHolding&&Input.GetKey(KeyCode.H))
            {
                
                ShootAngle();
            }
            else if (Input.GetKeyUp(KeyCode.H))
            {
                
                isHolding = false;
                
                ThrowBullet();
            }
        }
    }

    public void ReverseDirection()
    {
        directionRotation *= -1;
    }

    public void ShootAngle()
    {
        angle = 90f * Time.deltaTime * directionRotation;
        oldBullet.transform.RotateAround(pointRotation.position, Vector3.forward, angle);
        totalAngle += angle;
        
    }
    public void InitBullet()
    {
        oldBullet=Instantiate(arrow, bullerPos.position, bullerPos.rotation , bullerPos);
    }

    public void ThrowBullet()
    {
        // Chuyển từ độ sang radian
        float angleInRadians = totalAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
        newBullet = Instantiate(bullet, bullerPos.position, bullerPos.rotation);
        Debug.Log(direction);   
        if (facingRight)
        {
            newBullet.GetComponent<Rigidbody2D>().AddForce(direction * 10f, ForceMode2D.Impulse);
            totalAngle = 0f;
        }
        else
        {
            newBullet.GetComponent<Rigidbody2D>().AddForce(direction * 10f *-1f, ForceMode2D.Impulse);
            totalAngle = 0f;
        }
       
        Destroy(oldBullet);
        Destroy(newBullet, 2f);
    }


    public void ReduceHp(float hp)
    {

        this.hp -= hp;
        healthBar.SetHealthByImage(maxHp, this.hp);
        if (this.hp <= 0)
        {
            OnDead();

        }

    }

    public void OnInit()
    {
        isDead = false;
        this.hp = 100f;
        attackArea.SetActive(false);
        healthBar.SetHealthByImage(maxHp, hp);
        transform.position = savePoint;
        SavePoint();
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    public void Attack()
    {

        ChangeAnim("Attack");
        isAttack = true;
        attackArea.SetActive(true);
        StartCoroutine(ReturnIdle());
    }


    IEnumerator ReturnIdle()
    {
        couterTime.OnStart(Attack, .6f);
        yield return new WaitForSeconds(.4f);
        ChangeAnim("Idle");
        yield return new WaitForSeconds(.2f);
        attackArea.SetActive(false);
        isAttack = false;
        //attackCountDown = 0;
    }

    public void Dash()
    {
        speed = speed + 5f;
        
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

    }

    public void OnDead()
    {
        isDead = true;
        couterTime.OnCancel();
        StartCoroutine(SpawnHero());
    }

    IEnumerator SpawnHero()
    {
        //isAttack = true;
        yield return new WaitForSeconds(2f);
        OnInit();
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
