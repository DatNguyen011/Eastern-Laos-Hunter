using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI gold;
    public Button btnSettings;
    public Button btnClose;
    public Button btnMainMenu;
    public Button btnReplay;
    public GameObject settingPanel;

    void Start()
    {
        InitGold();
        btnSettings.onClick.AddListener(() => OpenSetting());
        btnMainMenu.onClick.AddListener(() => OpenMainMenu());
        btnClose.onClick.AddListener(() => {settingPanel.SetActive(false);});
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    public void Replay()
    {

    }

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

    public void InitGold()
    {
            gold.text = PlayerPrefs.GetString("gold");    
    }

    void Update()
    {

    }
}
