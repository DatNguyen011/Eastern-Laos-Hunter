using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public float gold;
    public List<Bot> listBot = new List<Bot>();
    public List<Transform> listTransformBot = new List<Transform>();
    public List<Bot> listBotPrefabs = new List<Bot>();
    public GameObject switchLevel;
    //public Bot botPrefabs;
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
            for (int j = 0; j < listBotPrefabs.Count; j++)
            {
                for (int i = 0; i < listTransformBot.Count; i++)
                {

                    Bot spawnBot = Instantiate(listBotPrefabs[j]);
                    spawnBot.transform.position = listTransformBot[i].position;
                    spawnBot.startPoint = listTransformBot[i].position;
                    //spawnBot.OnInit();
                    listBot.Add(spawnBot);
                }

            }
        }


    }

    public void OpenNextLevel()
    {
       
        if(listBot.Count <= 0)
        {
            switchLevel.SetActive(true);
        }
    }

    void Update()
    {
        OpenNextLevel();
    }
}
