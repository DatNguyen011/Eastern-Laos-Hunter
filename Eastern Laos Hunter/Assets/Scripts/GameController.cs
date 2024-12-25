using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public float gold;
    public List<Bot> listBot = new List<Bot>();
    public List<Transform> listTransformBot = new List<Transform>();
    public Bot botPrefabs;
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
        OnInit();
        InitGold();
    }

    public void OnInit()
    {
        if (listTransformBot.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Bot spawnBot = Instantiate(botPrefabs);
                spawnBot.transform.position = listTransformBot[i].position;
                //spawnBot.OnInit();
                listBot.Add(spawnBot);
            }
        }


    }

    void Update()
    {

    }
}
