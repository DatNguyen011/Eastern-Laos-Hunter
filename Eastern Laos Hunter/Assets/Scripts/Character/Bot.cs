using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : AtractBot
{
    private string currentAnim;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public override void OnAttack()
    {
        
    }

    public override void OnDead()
    {
        
    }

    public override void OnInit()
    {
        
    }
}
