using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBot : Bot
{
    public GameObject slimePrefab; 
    public Transform dropPoint;
    private GameObject newSmile;


    void Start()
    {
        OnInit();
    }

    void Update()
    {
        UpdateState();
    }

    public override void OnInit()
    {
        maxHp = 120f;
        hp = 120f;
        healthBar.SetHealth(maxHp, hp);
        base.OnInit();
    }

    public override void OnDead()
    {
        base.OnDead();
        Destroy(newSmile,3f);
    }

    public override void OnAttack()
    {

        
        base.OnAttack();
    }

    void SpawnSlime()
    {
        Vector2 currentPosition = transform.position;
        float randomAngle = Random.Range(0f, 360f);
        float angleInRadians = randomAngle * Mathf.Deg2Rad;
        Vector3 randomPoint = new Vector3(
            currentPosition.x + Mathf.Cos(angleInRadians) * 1f,
            currentPosition.y + Mathf.Sin(angleInRadians) * 1f, 0
        );
        newSmile = MyPoolManager.InstancePool.Get(slimePrefab, randomPoint);
        slimePrefab.SetActive(true);
        StartCoroutine(DisableSlime(newSmile, 5f));
    }

    private IEnumerator DisableSlime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        bullet.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            SpawnSlime();
        }
        if (collision.tag == "Wall" || collision.tag == "Tree")
        {
            isTouchWall = true;
        }
    }
}
