using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public IState<MiniBoss> currentState;
    public Animator anim;
    private string currentAnim;
    public GameObject attackArea;
    public bool isTouchWall=false;
    public float speed;
    public Vector2 newPosition;
    public GameObject heroGameOject;
    public Vector2 direction;
    private float hp = 200f;
    private float maxHp = 200f;
    public HealthBar healthBar;
    public bool isDead=false;
    public GameObject bulletPrefab;
    private Vector2[] directions = {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right,
        new Vector2(1, 1).normalized, new Vector2(-1, 1).normalized,
        new Vector2(1, -1).normalized, new Vector2(-1, -1).normalized
    };

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new MiniBossIdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void OnAttack()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        StartCoroutine(WaitScroll());
    }

    public void ThrowBullet()
    {
        foreach(Vector2 dir in directions)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = dir*5f;
            Destroy(newBullet,4f);
        }
    }

    IEnumerator WaitScroll()
    {
        yield return new WaitForSeconds(3f);
        
        
        ChangeState(new MiniBossIdleState());
    }

    public void ReduceHp(float dame)
    {
        ChangeState(new MiniBossHitState());
        StartCoroutine(DisableHitState());
        hp -= dame;
        healthBar.SetHealth(maxHp, hp);
        if (hp <= 0)
        {
            isDead = true;
            ChangeState(new MiniBossDeadState());
        }
    }

    IEnumerator DisableHitState()
    {
        yield return new WaitForSeconds(0.4f);
        ChangeState (new MiniBossIdleState());
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

    public void ChangeState(IState<MiniBoss> newState)
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
