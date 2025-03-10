﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Bot : BaseBot
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
    public bool isThrow = false;
    public Rigidbody2D rb;
    public bool haveTarget = false;
    public GameObject bloodBottle;
    public GameObject manaBottle;
    public GameObject goldOject;
    public GameObject attackArea;
    public Vector2 startPoint;
    public string typeBot;
    public GameObject floatingDamage;
    public GameObject patrolVFXPrefab;
    public Transform botVFXParent;
    public GameObject botVFX;


    void Start()
    {

        ChangeAnim("Idle");

    }

    public void UpdateState()
    {
        if (currentState != null && isDead == false)
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
        hp -= dame;
        if (hp <= 0)
        {
            isDead = true;
            OnDead();
        }
        healthBar.SetHealth(maxHp, hp);
        GameObject textFloating = Instantiate(floatingDamage, transform.position, Quaternion.identity);
        textFloating.transform.GetChild(0).GetComponent<TextMeshPro>().text = dame.ToString();
        ChangeState(new HitState());
        //StartCoroutine(DisableHitState());
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
        ChangeState(null);
        ChangeAnim("Dead");
        StartCoroutine(WaitDead());
    }

    IEnumerator WaitDead()
    {
        yield return new WaitForSeconds(2f);
        GameController.Instance.listBot.Remove(this);
        float randomNumber = Random.Range(1, 4);
        if (randomNumber == 1)
        {
            GameObject newBloodBottle = Instantiate(bloodBottle, transform.position, Quaternion.identity);
        }
        else if (randomNumber == 2)
        {
            GameObject newManaBottle = Instantiate(manaBottle, transform.position, Quaternion.identity);
        }
        else if (randomNumber == 3)
        {
            GameObject newGold = Instantiate(goldOject, transform.position, Quaternion.identity);
        }
        else if (GameController.Instance.listBot.Count <= 0)
        {
            GameController.Instance.OpenNextLevel();
        }
        Destroy(gameObject);

    }

    public override void OnInit()
    {
        ChangeState(new IdleState());
    }

    public Vector2 RandomPoint()
    {
        Vector2 currentPosition = transform.position;
        float randomAngle = Random.Range(0f, 360f);
        float angleInRadians = randomAngle * Mathf.Deg2Rad;
        Vector2 randomPoint = new Vector2(
            currentPosition.x + Mathf.Cos(angleInRadians) * 3f,
            currentPosition.y + Mathf.Sin(angleInRadians) * 3f
        );
        return randomPoint;
    }

    public void SpawnVfx()
    {
        //if (patrolVFX != null)
        //{
        //    Destroy(patrolVFX);
        //}
        Instantiate(patrolVFXPrefab, botVFXParent.transform.position, Quaternion.identity, botVFXParent);
    }

    public void SetDestination(Vector2 des)
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, des, speed * Time.deltaTime);
        Vector2 direction = des - (Vector2)transform.position;
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


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
            ChangeState(new AttackState());
        }
        if (collision.tag == "Wall" || collision.tag == "Tree")
        {
            isTouchWall = true;
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
