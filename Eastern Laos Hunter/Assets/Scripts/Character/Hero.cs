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
    public bool isDead = false;
    private bool isHit = false;
    public float countDownDash = 5f;
    public float dashTime = 0.3f;
    public CounterTime couterTime = new CounterTime();
    public float maxHp = 100f;
    public float maxMp = 100f;
    public float hp = 100f;
    public float mp = 100f;
    public HealthBar healthBar;
    public GameObject bullet;
   
    public Transform bullerPos;
    private GameObject newBullet;
    public Transform pointRotation;
    //private float directionRotation = 1f;
    private bool overMana = false;
    public GameObject dialogBox;
    public Rigidbody2D rb;
    private Vector2 move;
    private int atkNumber = 1;
    public PositionValue positionValue;
    public GameObject healthAnimPrefab;
    public Transform heroParent;
    public List<Image> skillImages = new List<Image>();
    public Collider2D heroCollider;
    public GameObject floatingDamage;
    public Transform heroVfxParent;
    void Start()
    {
        OnInit("hp", "mp");
    }
    void Update()
    {
        if (dialogBox.activeSelf)
        {
            rb.velocity = Vector2.zero;
            ChangeAnim("Idle");
            return;
        }

        if (!isAttack && !isDead && !isHit)
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

            if (move != Vector2.zero)
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

            if (Input.GetKeyDown(KeyCode.H) && !skillCooldowns[0])
            {
                couterTime.OnExecute();
                rb.velocity = Vector2.zero;
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.K) && !skillCooldowns[2] && !overMana)
            {
                couterTime.OnCancel();
                Dash();
            }
            else if (Input.GetKeyDown(KeyCode.L) && !skillCooldowns[3] && !overMana)
            {
                FinalAttack();
            }
            else if (Input.GetKeyDown(KeyCode.J) && !overMana && !skillCooldowns[1])
            {
                couterTime.OnCancel();
                rb.velocity = Vector2.zero;
                ChangeAnim("Idle");
                Transform target = FindNearestTarget();
               
                if (target != null)
                {
                    ThrowBullet(target);
                }
                else
                {
                    GameObject textFloatingGold = Instantiate(floatingDamage, transform.position, Quaternion.identity, heroVfxParent);
                    textFloatingGold.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Can not target!!!";
                }
            }

        }
    }

    private Transform FindNearestTarget()
    {
        float minDistance = float.MaxValue;
        Transform nearestTarget = null;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 7f, LayerMask.GetMask("Bot"));

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTarget = hit.transform;
            }
        }
        return nearestTarget;
    }


    public void ThrowBullet(Transform target)
    {
        skillCooldowns[1] = true;
        if (target == null) return;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.throwSound);

        Vector2 direction = (target.position - transform.position).normalized;
        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180f, 0);
        }
        newBullet = Instantiate(bullet, bullerPos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction * 10f;

        ReduceMp(20f);
        Destroy(newBullet, 2f);
        
        isAttack = false;
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

    public void SaveHP()
    {
        PlayerPrefs.SetFloat("hp", hp);
        PlayerPrefs.SetFloat("mp", mp);
        PlayerPrefs.Save();
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

    public void OnInit(string hp, string mp)
    {
        gameObject.tag = "Hero";
        rb = GetComponent<Rigidbody2D>();
        this.hp = PlayerPrefs.GetFloat(hp);
        this.mp = PlayerPrefs.GetFloat(mp);
        this.maxHp = PlayerPrefs.GetFloat("maxhp");
        this.maxMp = PlayerPrefs.GetFloat("maxmp");
        isDead = false;
        healthBar.SetHealthByImage(this.maxHp, this.hp);
        healthBar.SetManaByImage(this.maxMp, this.mp);
        positionValue.initPosValue = new Vector2(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"));
        
        transform.position = positionValue.initPosValue;
        heroCollider.gameObject.SetActive(true);
    }

    public void FinalAttack()
    {
        heroCollider.enabled = false;
        ChangeAnim("FinalAttack");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.finalAttackSound);
        isAttack = true;
        skillCooldowns[3] = true;
        StartCoroutine(WaitFinalAttack());
        StartCoroutine(Cooldown(5f, 3));
    }

    IEnumerator WaitFinalAttack()
    {
        yield return new WaitForSeconds(1f);
        heroCollider.enabled = true;
        Vector2 attackDirection = facingRight ? Vector2.right : Vector2.left; 
        float attackRange = 3f; 
        Vector2 boxSize = new Vector2(3f, 2f); 
        float angle = 0f;
        LayerMask enemyLayer = LayerMask.GetMask("Bot", "Box");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, angle, attackDirection, attackRange, enemyLayer);
        foreach (RaycastHit2D hit in hits) 
        {
            if (hit.collider.CompareTag("Bot"))
            {
                hit.collider.GetComponent<Bot>()?.ReduceHp(50f);
            }
            else if (hit.collider.CompareTag("MiniBoss"))
            {
                hit.collider.GetComponent<Boss>()?.ReduceHp(50f);
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
        heroCollider.enabled = false;
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
        LayerMask enemyLayer = LayerMask.GetMask("Bot","Box"); 

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, angle, attackDirection, attackRange, enemyLayer);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Bot"))
            {
                hit.collider.GetComponent<Bot>().ReduceHp(30f);
            }
            else if (hit.collider.CompareTag("MiniBoss"))
            {
                hit.collider.GetComponent<Boss>().ReduceHp(30f);
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
        heroCollider.enabled = true;
        isAttack = false;
    }

    public void Dash()
    {
        heroCollider.enabled = false;
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, move.normalized, 0.5f, LayerMask.GetMask("Wall"));
            if (hit.collider != null)
            {
                break;
            }

            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / dashTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        heroCollider.enabled = true;
        ReduceMp(10f);
        isAttack = false;
    }

    public void OnDead()
    {
        isDead = true;
        ChangeAnim("Dead");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.deadSound);
        gameObject.tag = "Untagged";
        gameObject.layer = 0;
        rb.velocity = Vector2.zero;
        UIManager.deadNumber += 1;
        PlayerPrefs.SetFloat("dead_number",UIManager.deadNumber);
        PlayerPrefs.Save();
        if(UIManager.deadNumber > 2)
        {
            UIManager.Instance.endGamePanel.SetActive(true);
            UIManager.Instance.OpenEndGame();
        }
        couterTime.OnCancel();
        GameController.Instance.StartCoroutine(SpawnHero());
    }

    IEnumerator SpawnHero()
    {
        yield return new WaitForSeconds(2f);
        heroCollider.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        OnInit("maxhp", "maxmp");
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
                randomValue = UnityEngine.Random.Range(30, 50);
                GameObject textFloatingHp = Instantiate(floatingDamage, transform.position, Quaternion.identity, heroVfxParent);
                textFloatingHp.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+ " + randomValue+"HP";
                AddHp(randomValue);
                break;

            case "ManaBottle":
                randomValue = UnityEngine.Random.Range(30, 50);
                GameObject textFloatingMana = Instantiate(floatingDamage, transform.position, Quaternion.identity, heroVfxParent);
                textFloatingMana.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+ " + randomValue+"MP";
                AddMp(randomValue);
                break;

            case "Gold":
                Instantiate(healthAnimPrefab, heroParent.transform.position, Quaternion.identity, heroParent);
                randomValue = UnityEngine.Random.Range(5, 8);
                GameObject textFloatingGold = Instantiate(floatingDamage, transform.position, Quaternion.identity, heroVfxParent);
                textFloatingGold.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+ "+randomValue+" Gold";
                GameController.Instance.GainGold(randomValue);
                break;

            case "RedStone":
                Instantiate(healthAnimPrefab, heroParent.transform.position, Quaternion.identity, heroParent);
                randomValue = UnityEngine.Random.Range(10, 20);
                GameObject textFloating = Instantiate(floatingDamage, transform.position, Quaternion.identity, heroVfxParent);
                textFloating.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+" + randomValue +"% HP";
                maxHp += randomValue;
                healthBar.SetHealthByImage(maxHp,this.hp);
                break;

            case "BlueStone":
                Instantiate(healthAnimPrefab, heroParent.transform.position, Quaternion.identity, heroParent);
                randomValue = UnityEngine.Random.Range(10, 20);
                GameObject textFloatingBlue = Instantiate(floatingDamage, transform.position, Quaternion.identity, heroVfxParent);
                textFloatingBlue.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+" + randomValue + "% MP";
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
