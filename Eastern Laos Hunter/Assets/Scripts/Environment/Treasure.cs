using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Animator anim;
    public GameObject stone;

    public void OpenTreasure()
    {
        anim.SetTrigger("Open");

        GameObject newBloodBottle = Instantiate(stone, transform.position, transform.rotation);

        
    }
}
