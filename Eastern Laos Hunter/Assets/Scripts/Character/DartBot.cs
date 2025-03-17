using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBot : Bot
{
    public GameObject bulletPrefabs;
    public GameObject throwDart;
    
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
        maxHp = 100;
        hp = 100;
        healthBar.SetHealth(maxHp, hp);
        typeBot = "longrange";
        base.OnInit();
    }

    public override void OnDead()
    {
        base.OnDead();
    }

    public override void OnAttack()
    {
        isThrow = true;
        BotDirection();
        Vector2 direction = Hero.Instance.transform.position - transform.position;
        direction = direction.normalized;
        GameObject newBullet = Instantiate(bulletPrefabs, throwDart.transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction * 5f;
        Destroy(newBullet, 4f);
        StartCoroutine(WaitShoot());
        base.OnAttack();
    }

    IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(4f);
        isThrow = false;
    }
}
