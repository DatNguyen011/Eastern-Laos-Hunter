using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Bot : AtractBot
{
    //public GameObject player;
    private string currentAnim;
    public Animator anim;
    private float speed = 4f;
    private float distance;
    public HealthBar healthBar;
    public float hp = 100f;
    public float maxHp = 100f;
    public IState<Bot> currentState;
    public bool isDead = false;
    public bool isTouchWall = false;
    public Rigidbody2D rb;
    public bool haveTarget = false;
    public GameObject bloodBottle;
    public GameObject manaBottle;
    public GameObject goldOject;
    public GameObject attackArea;
    public Vector2 startPoint;
    public string typeBot;
    // Start is called before the first frame update

    void Start()
    {

        ChangeAnim("Idle");
           
    }

    public void UpdateState()
    {
        if (currentState != null)
        {

            currentState.OnExecute(this);
        }
    }

    public void FindPlayer()
    {
        distance = Vector2.Distance(Hero.Instance.transform.position, transform.position);
        float stopDistance = distance + 1f;
        Vector2 direction = Hero.Instance.transform.position - transform.position;
        direction = direction.normalized;
        // tinh goc giua vector(x,y) va truc Ox
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle >= -90 && angle <= 90)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);

        }

        if (distance < 5f)
        {

            if (distance <= 1.5f)
            {
                ChangeState(new IdleState());
            }
            transform.position = Vector2.MoveTowards(this.transform.position, Hero.Instance.transform.position, speed * Time.deltaTime);
        }
    }


    public void ReduceHp(float dame)
    {
        ChangeState(new HitState());
        StartCoroutine(DisableHitState());
        hp -= dame;
        healthBar.SetHealth(maxHp, hp);
        if (hp <= 0)
        {
            isDead = true;
            OnDead();
        }
    }

    IEnumerator DisableHitState()
    {
        yield return new WaitForSeconds(.4f);
        ChangeState(new IdleState());
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void ChangeState(IState<Bot> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }



    public override void OnAttack()
    {
       attackArea.SetActive(false);
       ChangeState(new IdleState());
    }

    public override void OnDead()
    {
        isDead = true;
        ChangeState(null);
        ChangeAnim("Dead");
        StartCoroutine(WaitDead());
    }

    IEnumerator WaitDead()
    {
        yield return new WaitForSeconds(3f);
        GameController.Instance.listBot.Remove(this);
        float randomNumber = Random.Range(1, 4);
        if (randomNumber == 1)
        {
            GameObject newBloodBottle = Instantiate(bloodBottle, transform.position, transform.rotation);
        }
        else if (randomNumber == 2)
        {
            GameObject newManaBottle = Instantiate(manaBottle, transform.position, transform.rotation);
        }
        else if (randomNumber == 3)
        {
            GameObject newGold = Instantiate(goldOject, transform.position, transform.rotation);
        }
        Destroy(gameObject);

    }

    public override void OnInit()
    {
        ChangeState(new IdleState());
    }

    public Vector2 RandomPoint()
    {
        // Vị trí hiện tại
        Vector2 currentPosition = transform.position;

        // Random một góc trong khoảng từ 0 đến 360 độ
        float randomAngle = Random.Range(0f, 360f);

        // Chuyển góc random sang radian
        float angleInRadians = randomAngle * Mathf.Deg2Rad;

        // Tính toán vị trí mới dựa trên góc và bán kính
        Vector2 randomPoint = new Vector2(
            currentPosition.x + Mathf.Cos(angleInRadians) * 3f,
            currentPosition.y + Mathf.Sin(angleInRadians) * 3f
        );
        return randomPoint;
    }

    public void SetDestination(Vector2 des)
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, des, speed*Time.deltaTime);
        Vector2 direction = des - (Vector2)transform.position;
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle >= -90 && angle <= 90)
        {
            transform.localScale = new Vector3(-1, 1, 1 );
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            haveTarget = true;
            ChangeState(new AttackState());
            //StartCoroutine(AttackWait());
        }
        if (collision.tag == "Wall" || collision.tag == "Tree")
        {
            isTouchWall= true;
        }
    }


    IEnumerator AttackWait()
    {
        if (haveTarget == true)
        {
            ChangeState(new AttackState());
            yield return new WaitForSeconds(5f);
            StartCoroutine(AttackWait());
        }

    }
}
