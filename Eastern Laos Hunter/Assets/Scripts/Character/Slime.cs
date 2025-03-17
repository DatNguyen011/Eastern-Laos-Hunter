using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damagePerSecond = 5f;
    private bool isHeroInside = false;

    private void Start()
    {
        StartCoroutine(waitSetActive());
    }

    IEnumerator waitSetActive()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            isHeroInside = true;
            StartCoroutine(ApplyDamage(collision.GetComponent<Hero>()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            isHeroInside = false;
        }
    }

    private IEnumerator ApplyDamage(Hero hero)
    {
        while (isHeroInside)
        {
            if (hero != null)
            {
                hero.ReduceHp(damagePerSecond);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
