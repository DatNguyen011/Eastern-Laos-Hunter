using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Animator anim;
    public GameObject stone;
    private bool isOpen;

    public void OpenTreasure()
    {
        if (!isOpen)
        {
            anim.SetTrigger("Open");
            GameObject newBloodBottle = Instantiate(stone, transform.position, transform.rotation);
            isOpen = true;
        }
    }
}
