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
    public float maxMp = 100f;
    public float hp = 100f;
    public float mp = 100f;
    public HealthBar healthBar;
    //public GameObject checkPoint;
    private Vector3 savePoint;
    public GameObject bullet;
    public GameObject arrow;
    public Transform bullerPos;
    private bool isHolding=false;    //public List<Vector2> checkPoints = new List<Vector2>();
    private GameObject newBullet;
    private GameObject oldBullet;
    public Transform pointRotation;
    private float angle;
    private float totalAngle;
    //private float maxAngle=60f;
    private float directionRotation=1f;
    private bool overMana=false;
    public GameObject dialogBox;
    public Rigidbody2D rb;

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
        if (isAttack==false&&isDead==false&&dialogBox.activeSelf==false)
        {
            rb = GetComponent<Rigidbody2D>();
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector2 move = new Vector2(moveX, moveY);
            rb.velocity = move * speed;
       

            if (move == Vector2.zero)
            {
                ChangeAnim("Idle");
            }

            if (move != Vector2.zero&&isHolding==false)
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
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                couterTime.OnExecute();
                rb.velocity = Vector2.zero;
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {

                couterTime.OnCancel();
                Dash();
            }
            else if(Input.GetKeyDown(KeyCode.H)&&!overMana)
            {
                isHolding = true;
                rb.velocity = Vector2.zero;
                ChangeAnim("Idle");
                InitBullet();

            }
            else if (isHolding&&Input.GetKey(KeyCode.H) && !overMana)
            {
                rb.velocity = Vector2.zero;
                ShootAngle();
            }
            else if (Input.GetKeyUp(KeyCode.H) && !overMana)
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
        
        angle = 50f * Time.deltaTime * directionRotation;
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
        ReduceMp(10f);
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

    public void ReduceMp(float mp)
    {

        this.mp -= mp;
        healthBar.SetManaByImage(maxMp, this.mp);
        if(this.mp < mp) {
            overMana = true;
        }
    }
    public void AddMp(float mp)
    {
        
        this.mp += mp;
        if (this.mp > maxMp)
        {
            this.mp = maxMp;
        }
        if (this.mp > 10f)
        {
            overMana=false;
        }
        healthBar.SetManaByImage(maxMp, this.mp);

    }
    public void AddHp(float hp)
    {
        
        this.hp += hp;
        if (this.hp > maxHp)
        {
            this.hp = maxHp;
        }
        healthBar.SetHealthByImage(maxHp, this.hp);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HealthBottle")
        {
            float randomHp = UnityEngine.Random.Range(10f, 30f);
            AddHp(randomHp);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "ManaBottle")
        {
            float randomMp = UnityEngine.Random.Range(10f, 30f);
            AddMp(randomMp);
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Gold")
        {
            float randomGold= UnityEngine.Random.Range(5, 10);
            GameController.Instance.GainGold(randomGold);
            Destroy(collision.gameObject);
        }
    }
}
