using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Animator anim;
    private string currentAnim="BoxIdle";
    public GameObject bloodBottle;
    public GameObject manaBottle;
    public GameObject goldOject;
    public Vector2 startPoint;
    private void Start()
    {
        
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

    public void DestroyBox()
    {
        ChangeAnim("Broken");
        float randomNumber = Random.Range(1, 6);
        if (randomNumber == 1)
        {
            GameObject newBloodBottle = Instantiate(bloodBottle, transform.position, transform.rotation);
        }
        else if (randomNumber == 2)
        {
            GameObject newManaBottle = Instantiate(manaBottle, transform.position, transform.rotation);
        }
        else if (randomNumber == 3)
        {
            GameObject newGold = Instantiate(goldOject, transform.position, transform.rotation);
        }
        Destroy(gameObject,.5f);
    }

}
