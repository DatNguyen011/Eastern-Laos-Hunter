using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtractBot : MonoBehaviour
{
    public abstract void OnInit();

    public abstract void OnAttack();

    public abstract void OnDead();

}
