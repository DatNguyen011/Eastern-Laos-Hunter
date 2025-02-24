using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI gold;
    public TextMeshProUGUI time;
    public TextMeshProUGUI deadNumber;
    public Button btnSettings;
    public Button btnClose;
    public Button btnMainMenu;
    public Button btnReplay;
    public GameObject settingPanel;
    private float elapsedTime = 0;

    void Start()
    {
        OnInit();
        btnSettings.onClick.AddListener(() => OpenSetting());
        btnMainMenu.onClick.AddListener(() => OpenMainMenu());
        btnClose.onClick.AddListener(() => {settingPanel.SetActive(false);});
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        time.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

    public void OnInit()
    {
        gold.text = PlayerPrefs.GetString("gold");
    }
}
