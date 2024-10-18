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
        btnFlash.onClick.AddListener(() => Flash());
    }

    private void Attack()
    {
        Hero.Instance.ChangeAnim("Attack");
    }

    private void Flash()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
