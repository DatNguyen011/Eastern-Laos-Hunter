using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public float gold;
    public List<BaseBot> listBot = new List<BaseBot>();
    public List<Transform> listTransform = new List<Transform>();
    //public List<Transform> listTransformBoss = new List<Transform>();
    public List<Bot> listBotPrefabs = new List<Bot>();
    public List<Boss> listBossPrefabs = new List<Boss>();
    [SerializeField] public List<Box> listBoxPrefabs = new List<Box>();
    [SerializeField] private GameObject wall;
    public GameObject switchLevel;
    [SerializeField] private GameObject spawnVFXPrefabs;

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
        if (!IsScenePlayed(PlayerPrefs.GetInt("sceneNumber")))
        {
            OpenNextLevel();
        }
        InitGold();
    }

    public bool IsScenePlayed(int sceneIndex)
    {
        return PlayerPrefs.GetInt("scene_" + sceneIndex) == 0;
    }

    public void OnInit(int sceneIndex)
    {
        if (IsScenePlayed(sceneIndex))
        {
            if (wall != null)
            {
                wall.SetActive(true);
            }

            List<Transform> availablePositions = new List<Transform>(listTransform);
            int index = 0;

            if (listBotPrefabs.Count > 0)
            {
                for (int i = 0; i < listBotPrefabs.Count && index < availablePositions.Count; i++, index++)
                {
                    StartCoroutine(WaitPlayVFX(i));
                    Bot spawnBot = Instantiate(listBotPrefabs[i]);
                    spawnBot.transform.position = availablePositions[index].position;
                    spawnBot.startPoint = listTransform[i].position;
                    listBot.Add(spawnBot);
                }
            }

            if (listBossPrefabs.Count > 0)
            {
                for (int i = 0; i < listBossPrefabs.Count && index < availablePositions.Count; i++, index++)
                {
                    StartCoroutine(WaitPlayVFX(i));
                    Boss spawnBoss = Instantiate(listBossPrefabs[i]);
                    spawnBoss.transform.position = availablePositions[index].position;
                    spawnBoss.startPoint = listTransform[i].position;
                    listBot.Add(spawnBoss);
                }
            }

            if (listBoxPrefabs.Count > 0)
            {
                for (int i = 0; i < listBoxPrefabs.Count && index < availablePositions.Count; i++, index++)
                {
                    StartCoroutine(WaitPlayVFX(i));
                    Box spawnBox = Instantiate(listBoxPrefabs[i]);
                    spawnBox.transform.position = availablePositions[index].position;
                    spawnBox.startPoint = listTransform[i].position;
                }
            }
        }
        else if(!IsScenePlayed(sceneIndex)) return;
    }

    public IEnumerator WaitPlayVFX(int i)
    {
        GameObject spawnvfx = Instantiate(spawnVFXPrefabs, listTransform[i].position, Quaternion.identity, listTransform[i]);
        yield return new WaitForSeconds(.5f);
        Destroy(spawnvfx);
    }


    public void OpenNextLevel()
    {

        if (listBot.Count <= 0)
        {
            Hero.Instance.SaveHP();
            PlayerPrefs.SetInt("scene_" + SceneManager.GetActiveScene().buildIndex, 1);
            PlayerPrefs.SetFloat("time", UIManager.gameTime);

            PlayerPrefs.Save();
            switchLevel.SetActive(true);
            if (wall != null)
            {
                wall.SetActive(false);

            }
        }
    }
}
