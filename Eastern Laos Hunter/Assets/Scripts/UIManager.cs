using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI gold;
    
    void Start()
    {
        InitGold();
    }

    public void InitGold()
    {
            gold.text = PlayerPrefs.GetString("gold");    
    }

    void Update()
    {

    }
}
