using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBot : Bot
{
    public GameObject bulletPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    public override void OnInit()
    {
        healthBar.SetHealth(50, 50);
        typeBot = "longrange";
        base.OnInit();
    }

    public override void OnDead()
    {
        base.OnDead();
    }

    public override void OnAttack()
    {
        Vector2 direction = Hero.Instance.transform.position - transform.position;
        direction=direction.normalized;
        GameObject newBullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction * 5f;
        ChangeState(new PatrolState());
        base.OnAttack();
    }
}
