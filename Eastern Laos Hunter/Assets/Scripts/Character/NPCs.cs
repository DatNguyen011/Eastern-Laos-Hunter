using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCs : Singleton<NPCs>
{
    public Animator anim;
    public Transform botVFXParent;
    public GameObject patrolVFXPrefab;
    public DialogueObject dialogue;
    public GameObject dialogBox;
    private GameObject VFX;

    private void Start()
    {
        VFX = Instantiate(patrolVFXPrefab, botVFXParent.transform.position, Quaternion.identity, botVFXParent);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Hero")
        {
            dialogBox.SetActive(true);
            DialogUI.Instance.InitDialog(dialogue);
        }
    }

    public void DestroyNPC()
    {
        dialogBox.SetActive(false);
        anim.ResetTrigger("Idle");
        anim.SetTrigger("Dead");
        Destroy(VFX);
        Destroy(gameObject,.8f);
    }
}
