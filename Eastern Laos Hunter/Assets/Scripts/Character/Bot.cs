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
    private float hp=10f;
    private float maxHp=100f;
    // Start is called before the first frame update

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void FindPlayer()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction=direction.normalized;
        // tinh goc giua vector(x,y) va truc Ox
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);


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
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
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
