using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BaseBot
{
    public IState<Boss> currentState;
    public Animator anim;
    private string currentAnim;
    public GameObject attackArea;
    public bool isTouchWall=false;
    public float speed;
    public Vector2 newPosition;
    //public GameObject heroGameOject;
    public Vector2 direction;
    public float hp = 200f;
    public float maxHp = 200f;
    public HealthBar healthBar;
    public bool isDead=false;
    public GameObject bulletPrefab;
    public Vector2 startPoint;
    private Vector2[] directions = {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right,
        new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized,
        new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized
    };
    [SerializeField] private GameObject treasurePrefabs;

    void Start()
    {
        ChangeState(new BossIdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnAttack()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        StartCoroutine(WaitScroll());
    }

    public void ThrowBullet()
    {
        foreach (Vector2 dir in directions)
        {
            GameObject newBullet = MyPoolManager.InstancePool.Get(bulletPrefab, transform.position);
            newBullet.GetComponent<Rigidbody2D>().velocity = dir * 5f;
            newBullet.SetActive(true);
            StartCoroutine(DisableBullet(newBullet, 4f));
        }
    }

    private IEnumerator DisableBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        bullet.SetActive(false); 
    }

    IEnumerator WaitScroll()
    {
        yield return new WaitForSeconds(5f);
        ChangeState(new BossIdleState());
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
    }

    public void Direction()
    {
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

    public override void OnDead()
    {
        ChangeState(null);
        ChangeAnime("Dead");
        Instantiate(treasurePrefabs, transform.position, Quaternion.identity);
        GameController.Instance.listBot.Remove(this);
        if (GameController.Instance.listBot.Count <= 0)
        {
            GameController.Instance.OpenNextLevel();
        }
        Destroy(gameObject,1f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            isTouchWall = true;
        }
    }

    

    public void ChangeAnime(string newAnim)
    {
        if (currentAnim!=newAnim)
        {
            anim.ResetTrigger(newAnim);
            currentAnim = newAnim;
            anim.SetTrigger(currentAnim);
        }
    }

    public void ChangeState(IState<Boss> newState)
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

}
