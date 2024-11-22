using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public float gold;
    public void GainGold(float gold)
    {
        this.gold += gold;
        PlayerPrefs.SetString("gold", this.gold.ToString());
        UIManager.Instance.InitGold();
    }
    
    public void InitGold()
    {
        if (!PlayerPrefs.HasKey("gold"))
        {
            gold = 0;
            PlayerPrefs.SetString("gold", 0 + "");
        }
        else
        {
            gold = float.Parse(PlayerPrefs.GetString("gold"));
        }
    }

    void Start()
    {
        
        InitGold();
    }

    
    void Update()
    {
        
    }
}
