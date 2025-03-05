using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public float gold;
    public List<BaseBot> listBot = new List<BaseBot>();
    public List<Transform> listTransformBot = new List<Transform>();
    public List<Transform> listTransformBoss = new List<Transform>();
    public List<Bot> listBotPrefabs = new List<Bot>();
    public List<Boss> listBossPrefabs = new List<Boss>();
    [SerializeField] private GameObject wall;
    public GameObject switchLevel;

    public void GainGold(float gold)
    {
        this.gold += gold;
        PlayerPrefs.SetString("gold", this.gold.ToString());
        UIManager.Instance.OnInit();
    }

    public void InitGold()
    {
        if (!PlayerPrefs.HasKey("gold"))
        {
            gold = 0;
            PlayerPrefs.SetString("gold", "0");
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

    public void OnInit()
    {
        wall.SetActive(true);
        if (listTransformBot.Count > 0)
        {
            for (int i = 0; i < listBotPrefabs.Count; i++)
            {
                Bot spawnBot = Instantiate(listBotPrefabs[i]);
                spawnBot.transform.position = listTransformBot[i].position;
                spawnBot.startPoint = listTransformBot[i].position;
                listBot.Add(spawnBot);
            }
        }
        if (listTransformBoss.Count > 0)
        {
            for (int i = 0; i < listBossPrefabs.Count; i++)
            {
                    Boss spawnMiniBot = Instantiate(listBossPrefabs[i]);
                    spawnMiniBot.transform.position = listTransformBoss[i].position;
                    spawnMiniBot.startPoint = listTransformBoss[i].position;
                    listBot.Add(spawnMiniBot);
            }
        }
    }

    public void OpenNextLevel()
    {
        if (listBot.Count <= 0)
        {
            wall.SetActive (false);
            switchLevel.SetActive(true);
            
        }
    }

    void Update()
    {
        //OpenNextLevel();
    }
}
