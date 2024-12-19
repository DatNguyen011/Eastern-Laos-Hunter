using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Bot : AtractBot
{
    private string currentAnim;
    public Animator anim;
    private float speed = 3f;
    public float distance;
    public HealthBar healthBar;
    public float hp = 100f;
    public float maxHp = 100f;
    public IState<Bot> currentState;
    public bool isDead = false;
    public Rigidbody2D rb;
    public bool haveTarget = false;
    public GameObject bloodBottle;
    public GameObject manaBottle;
    public GameObject goldOject;

    void Start()
    {
        ChangeAnim("Idle");
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = direction.normalized*2f;
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
                rb.velocity = Vector2.zero;
                
            }
            Vector2 newPosition = Vector2.MoveTowards(rb.position, Hero.Instance.transform.position, speed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }

    public void ReduceHp(float dame)
    {
        ChangeState(new HitState());
        StartCoroutine(DisableHitState());
        hp -= dame;
        healthBar.SetHealth(maxHp, hp);
        if(hp <= 0)
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

    }

    public override void OnDead()
    {
        //GameController.Instance.GainGold(10f);
        float randomNumber = Random.Range(1, 4);
        if (randomNumber ==1 )
        {
            GameObject newBloodBottle = Instantiate(bloodBottle, transform.position, transform.rotation);
        }
        else if (randomNumber ==2 )
        {
            GameObject newManaBottle = Instantiate(manaBottle, transform.position, transform.rotation);
        }
        else if (randomNumber ==3 )
        {
            GameObject newGold = Instantiate(goldOject, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }

    public override void OnInit()
    {
        ChangeState(new IdleState());
    }

    public void SetDestination(Vector3 des)
    {
        transform.position = Vector3.MoveTowards(transform.position, des, 0.02f);
        Vector3 direction = des-transform.position;
        rb.velocity = direction.normalized * 2f;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        if (angle >= -90 && angle <= 90)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
            //ChangeState(new AttackState());
            StartCoroutine(AttackWait());
        }
    }


    IEnumerator AttackWait()
    {
        if(haveTarget==true)
        {
            ChangeState(new AttackState());
            yield return new WaitForSeconds(5f);
            StartCoroutine(AttackWait());
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            haveTarget = false;
            
            ChangeState(new IdleState());
        }

    }
}
