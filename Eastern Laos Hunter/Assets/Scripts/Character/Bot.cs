using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bot : AtractBot
{
    public GameObject player;
    private string currentAnim;
    private Animator anim;
    private float speed = 4f;
    private float distance;
    public HealthBar healthBar;
    public float hp = 100f;
    public float maxHp = 100f;
    public IState iState;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
       //if(iState !=null)
        //{
            //iState.OnExecute(this);
        //}

    }

    public void FindPlayer()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        float stopDistance = distance+1f;
        Vector2 direction = player.transform.position - transform.position;
        direction=direction.normalized;
        // tinh goc giua vector(x,y) va truc Ox
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        


        if (angle >= -90 && angle <= 90)
        {
            transform.localScale = new Vector3(-4, 4, 4);
            
        }
        else
        {
            transform.localScale = new Vector3(4, 4, 4);

        }
        
        if (distance < 5f)
        {

            if (distance <= 1.2f)
            {
                return;
            }
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public void ReduceHp(float dame)
    {
        hp -= dame;
        healthBar.SetHealth(maxHp, hp);
    }

    public void Moving()
    {
        //ChangeAnim("Hit");
        Vector2 randomPosition= new Vector2(Random.Range(1,10), Random.Range(1,10));
        
        Vector2 direction = randomPosition - (Vector2)transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, direction, speed*Time.deltaTime);
    }
    private void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }



    public override void OnAttack()
    {

    }

    public override void OnDead()
    {

    }

    public override void OnInit()
    {

    }
}
