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
    private Dictionary<int, bool> skillCooldowns = new Dictionary<int, bool>
    {
    { 0, false },
    { 1, false },
    { 2, false },
    { 3, false },
    };
    private bool isDead = false;
    private bool isHit = false;
    public float countDownDash = 5f;
    public float dashTime = 0.3f;
    public CounterTime couterTime = new CounterTime();
    public float maxHp = 100f;
    public float maxMp = 100f;
    public float hp = 100f;
    public float mp = 100f;
    public float dame = 20f;
    public HealthBar healthBar;
    //public GameObject checkPoint;
    private Vector3 savePoint;
    public GameObject bullet;
    public GameObject arrow;
    public Transform bullerPos;
    private bool isHolding = false;
    private GameObject newBullet;
    private GameObject oldBullet;
    public Transform pointRotation;
    private float angle;
    private float totalAngle;
    //private float maxAngle=60f;
    private float directionRotation = 1f;
    private bool overMana = false;
    public GameObject dialogBox;
    public Rigidbody2D rb;
    private Vector2 move;
    private Vector2 targetPosition;
    private int atkNumber = 1;
    public PositionValue positionValue;
    public GameObject healthAnimPrefab;
    public Transform heroParent;
    public List<Image> skillImages = new List<Image>();
    public Collider2D heroCollider;
    public GameObject floatingDamage;
    public Transform heroVfxParent;
    //public GameObject heroVfx;

    //public HealthBar healthBar;
    void Start()
    {

        OnInit();
    }

    //AWDS
    void Update()
    {
        if (isAttack == false && isDead == false && dialogBox.activeSelf == false && isHit == false)
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

            if (move != Vector2.zero && isHolding == false)
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

            if (Input.GetKeyDown(KeyCode.J) && skillCooldowns[0] == false)
            {
                couterTime.OnExecute();
                rb.velocity = Vector2.zero;
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.K) && skillCooldowns[2] == false && !overMana)
            {
                couterTime.OnCancel();
                Dash();
            }

            else if(Input.GetKeyDown(KeyCode.L)&& skillCooldowns[3] == false && !overMana)
            {
                FinalAttack();
            }

            else if (Input.GetKeyDown(KeyCode.H) && !overMana && skillCooldowns[1] == false)
            {
                couterTime.OnCancel();
                isHolding = true;
                
                rb.velocity = Vector2.zero;
                ChangeAnim("Idle");
                InitBullet();

            }
            else if (isHolding && Input.GetKey(KeyCode.H) && !overMana)
            {
                couterTime.OnCancel();
                rb.velocity = Vector2.zero;
                ShootAngle();
            }
            else if (Input.GetKeyUp(KeyCode.H) && !overMana)
            {
                couterTime.OnCancel();
                isAttack = true;
                isHolding = false;
                ChangeAnim("Throw");
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
        skillCooldowns[1] = true;
        oldBullet = Instantiate(arrow, bullerPos.position, bullerPos.rotation, bullerPos);
    }

    public void ThrowBullet()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.throwSound);
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
            newBullet.GetComponent<Rigidbody2D>().AddForce(direction * 10f * -1f, ForceMode2D.Impulse);
            totalAngle = 0f;
        }
        ReduceMp(20f);
        Destroy(oldBullet);
        Destroy(newBullet, 2f);
        StartCoroutine(ThrowAttack());
        StartCoroutine(Cooldown(3f, 1));
    }

    public IEnumerator ThrowAttack()
    {
        yield return new WaitForSeconds(.5f);
        isAttack = false;
    }

    public void ReduceHp(float hp)
    {
        this.hp -= hp;
        rb.velocity = Vector2.zero;
        isHit = true;
        healthBar.SetHealthByImage(maxHp, this.hp);

        ChangeAnim("Hit");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);
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
        if (this.mp < mp)
        {
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
            overMana = false;
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
        rb = GetComponent<Rigidbody2D>();
        isDead = false;
        this.hp = PlayerPrefs.GetFloat("hp");
        this.mp = PlayerPrefs.GetFloat("mp");
        this.maxHp = PlayerPrefs.GetFloat("maxhp");
        this.maxMp = PlayerPrefs.GetFloat("maxmp");
        healthBar.SetHealthByImage(maxHp, hp);
        healthBar.SetManaByImage(maxMp, mp);
        positionValue.initPosValue = new Vector2(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"));
        
        transform.position = positionValue.initPosValue;
        heroCollider.gameObject.SetActive(true);
    }

    public void FinalAttack()
    {
        ChangeAnim("FinalAttack");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.finalAttackSound);
        isAttack = true;
        skillCooldowns[3] = true;
        StartCoroutine(WaitFinalAttack());
        StartCoroutine(Cooldown(5f, 3));
    }

    IEnumerator WaitFinalAttack()
    {
        yield return new WaitForSeconds(0.6f); // Delay để khớp với animation

        Vector2 attackDirection = facingRight ? Vector2.right : Vector2.left; // Hướng của BoxCast
        float attackRange = 3f; // Chiều dài của hộp
        Vector2 boxSize = new Vector2(3f, 2f); // Kích thước của hộp (ngang x cao)
        float angle = 0f; // Góc quay của BoxCast (0 nghĩa là không xoay)
        LayerMask enemyLayer = LayerMask.GetMask("Bot"); // Lọc layer của kẻ địch

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, angle, attackDirection, attackRange, enemyLayer);

        foreach (RaycastHit2D hit in hits) // Kiểm tra từng kẻ địch trong vùng
        {
            if (hit.collider.CompareTag("Bot"))
            {
                hit.collider.GetComponent<Bot>()?.ReduceHp(dame);
            }
            else if (hit.collider.CompareTag("MiniBoss"))
            {
                hit.collider.GetComponent<Boss>()?.ReduceHp(dame);
            }
            else if (hit.collider.CompareTag("Box"))
            {
                hit.collider.GetComponent<Box>()?.DestroyBox();
            }
            else if (hit.collider.CompareTag("Treasure"))
            {
                hit.collider.GetComponent<Treasure>()?.OpenTreasure();
            }
        }

        isAttack = false;
    }

    public void Attack()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.attackSound);
        if (atkNumber > 3)
        {
            atkNumber = 1;
        }
        ChangeAnim("Attack" + atkNumber);

        atkNumber++;
        isAttack = true;
        skillCooldowns[0] = true;
        StartCoroutine(PerformAttack());
        StartCoroutine(Cooldown(.5f, 0));
    }

    

    IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(0.4f); 
        Vector2 attackDirection = facingRight ? Vector2.right : Vector2.left;
        float attackRange = 1.5f;
        Vector2 boxSize = new Vector2(1.5f, 1f); 
        float angle = 0f; 
        LayerMask enemyLayer = LayerMask.GetMask("Bot"); 

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, angle, attackDirection, attackRange, enemyLayer);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Bot"))
            {
                hit.collider.GetComponent<Bot>().ReduceHp(30f);
            }
            else if (hit.collider.CompareTag("MiniBoss"))
            {
                hit.collider.GetComponent<Boss>().ReduceHp(20f);
            }
            else if (hit.collider.CompareTag("Box"))
            {
                hit.collider.GetComponent<Box>().DestroyBox();
            }
            else if (hit.collider.CompareTag("Treasure"))
            {
                hit.collider.GetComponent<Treasure>()?.OpenTreasure();
            }
        }

        isAttack = false;
    }

    //IEnumerator ReturnIdle()
    //{
    //    couterTime.OnStart(Attack, .5f);
    //    yield return new WaitForSeconds(.3f);
    //    ChangeAnim("Idle");
    //    yield return new WaitForSeconds(.2f);
    //    isAttack = false;
    //    //attackCountDown = 0;
    //}

    public void Dash()
    {

        ChangeAnim("Tele1");

        skillCooldowns[2] = true;
        isAttack = true;
        skillImages[2].fillAmount = 1;
        StartCoroutine(Cooldown(3f, 2));

        StartCoroutine(Dashing());

    }
    IEnumerator Cooldown(float duration, int skillNumber)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            skillImages[skillNumber].fillAmount = Mathf.Lerp(1, 0, elapsed / duration);
            yield return null; 
        }
        skillImages[skillNumber].fillAmount = 0; 
        skillCooldowns[skillNumber] = false;
    }
    IEnumerator Dashing()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.dashSound);
        float dashTime = 0.4f; 
        float elapsedTime = 0f; 
        Vector2 startPos = transform.position; 
        Vector2 targetPos = startPos + move.normalized * 6f; 
        isAttack = true; 
        while (elapsedTime < dashTime)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / dashTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos; 
        ReduceMp(10f);
        isAttack = false; 
    }



    public void OnDead()
    {
        isDead = true;
        ChangeAnim("Dead");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.deadSound);
        heroCollider.gameObject.SetActive(false);
        UIManager.Instance.deadNumber += 1;
        PlayerPrefs.SetFloat("dead_number",UIManager.Instance.deadNumber);
        PlayerPrefs.Save();
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        int randomValue = 0;

        switch (collision.tag)
        {
            case "HealthBottle":
                randomValue = UnityEngine.Random.Range(90, 100);
                AddHp(randomValue);
                break;

            case "ManaBottle":
                randomValue = UnityEngine.Random.Range(90, 100);
                AddMp(randomValue);
                break;

            case "Gold":
                randomValue = UnityEngine.Random.Range(5, 10);
                GameController.Instance.GainGold(randomValue);
                break;

            case "RedStone":
                GameObject textFloating = Instantiate(floatingDamage, transform.position, Quaternion.identity, heroVfxParent);
                textFloating.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Increased HP";
                randomValue = UnityEngine.Random.Range(10, 20);
                maxHp += randomValue;
                healthBar.SetHealthByImage(maxHp,this.hp);
                break;

            case "BlueStone":
                randomValue = UnityEngine.Random.Range(10, 20);
                maxMp += randomValue;
                healthBar.SetManaByImage(maxMp, this.hp);
                break;
            case "YellowStone":
                randomValue = UnityEngine.Random.Range(10, 20);

                break;
            default:
                return;
        }

        Destroy(collision.gameObject);
    }

}
