using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btnAttack;
    public Button btnFlash;
    // Start is called before the first frame update
    void Start()
    {
        btnAttack.onClick.AddListener(() => Attack());
        btnFlash.onClick.AddListener(() => Dash());
    }

    private void Attack()
    {
        Hero.Instance.Attack();
        
    }

   

    

    private void Dash()
    {
        Hero.Instance.Dash();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
