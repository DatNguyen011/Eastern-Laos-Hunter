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
    public float speed = .005f;
    public Vector2 newPosition;
    public GameObject heroGameOject;
    public Vector2 direction;

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



    IEnumerator WaitScroll()
    {
        yield return new WaitForSeconds(3f);
        ChangeState(new MiniBossIdleState());
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
