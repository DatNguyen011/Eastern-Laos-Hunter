using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Singleton<Hero>
{
    [SerializeField] float speed = 5f;
    private bool facingRight = true;
    public Animator animator;
    private string currentAnim;
    private bool isAttack = false;
    private bool isDash = false;
    private bool isDead = false;
    private bool isHit = false;
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
    private Vector2 move;
    private Vector2 targetPosition;
    private int atkNumber=1;
    public PositionValue positionValue;
    public GameObject healthAnimPrefab;
    public Transform heroParent;

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
        if (isAttack==false&&isDead==false&&dialogBox.activeSelf==false&&isDash==false && isHit == false)
        {
            rb = GetComponent<Rigidbody2D>();
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            move = new Vector2(moveX, moveY).normalized;
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
                couterTime.OnCancel();
                isHolding = true;
                
                rb.velocity = Vector2.zero;
                 ChangeAnim("Idle");
                InitBullet();

            }
            else if (isHolding&&Input.GetKey(KeyCode.H) && !overMana)
            {
                couterTime.OnCancel();
                rb.velocity = Vector2.zero;
                ShootAngle();
            }
            else if (Input.GetKeyUp(KeyCode.H) && !overMana)
            {
                couterTime.OnCancel();
                isAttack=true;
                isHolding = false;
                ChangeAnim("Throw");
                ThrowBullet();
                //isAttack = false;
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
        ReduceMp(20f);
        Destroy(oldBullet);
        Destroy(newBullet, 2f);
        StartCoroutine(ThrowAttack());
        
    }

    public IEnumerator ThrowAttack()
    {
        yield return new WaitForSeconds(.5f);
        isAttack = false;
    }

    public void ReduceHp(float hp)
    {
        this.hp -= hp;
        rb.velocity=Vector2.zero;
        isHit = true;
        healthBar.SetHealthByImage(maxHp, this.hp);
        
        ChangeAnim("Hit");
        StartCoroutine(HitToIdle());
        if (this.hp <= 0)
        {
            OnDead();
        }
    }

    public IEnumerator HitToIdle()
    {
        yield return new WaitForSeconds(.5f);
        
        isHit = false;

    }

    public void ReduceMp(float mp)
    {
        this.mp -= mp;
        healthBar.SetManaByImage(maxMp, this.mp);
        if (this.mp < mp) {
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
        GameObject healthAnim = Instantiate(healthAnimPrefab, heroParent.transform.position, Quaternion.identity, heroParent);
        healthBar.SetManaByImage(maxMp, this.mp);

    }
    public void AddHp(float hp)
    {
        this.hp += hp;
        if (this.hp > maxHp)
        {
            this.hp = maxHp;
        }
        GameObject healthAnim = Instantiate(healthAnimPrefab, heroParent.transform.position, Quaternion.identity, heroParent);
        healthBar.SetHealthByImage(maxHp, this.hp);
    }

    public void OnInit()
    {
        rb=GetComponent<Rigidbody2D>();
        isDead = false;
        this.hp = PlayerPrefs.GetFloat("hp");
        this.mp = PlayerPrefs.GetFloat("mp");
        attackArea.SetActive(false);
        healthBar.SetHealthByImage(maxHp, hp);
        healthBar.SetManaByImage(maxMp, mp);
        positionValue.initPosValue = new Vector2(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"));
        transform.position = positionValue.initPosValue;
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    public void Attack()
    {
        if (atkNumber > 3)
        {
            atkNumber = 1;
        }
        ChangeAnim("Attack"+atkNumber);
        
        atkNumber++;
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
        
        ChangeAnim("Tele1");
        isDash = true;
        
        StartCoroutine(Dashing());
        
    }

    IEnumerator Dashing()
    {
        yield return new WaitForSeconds(.5f);
        targetPosition = (Vector2)transform.position + move * 3f;
        rb.position = targetPosition;
        ChangeAnim("Tele2");
        yield return new WaitForSeconds(.5f);
        ReduceMp(10f);
        isDash = false;
    }


    public void OnDead()
    {
        isDead = true;
        ChangeAnim("Dead");
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
            float randomHp = UnityEngine.Random.Range(90f, 100f);
            AddHp(randomHp);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "ManaBottle")
        {
            float randomMp = UnityEngine.Random.Range(90f, 100f);
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
