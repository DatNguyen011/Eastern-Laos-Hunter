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
    public TextMeshProUGUI tmpTime;
    public TextMeshProUGUI tmpdeadNumber;
    public Button btnSettings;
    public Button btnClose;
    public Button btnMainMenu;
    public Button btnReplay;
    public GameObject settingPanel;
    private float elapsedTime = 0;
    public float deadNumber=0;

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
        PlayerPrefs.SetFloat("time", elapsedTime);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        tmpTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        tmpdeadNumber.text=deadNumber.ToString();
    }

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

    public void OnInit()
    {
        if (PlayerPrefs.HasKey("time"))
        {
            elapsedTime = PlayerPrefs.GetFloat("time");
        }
        gold.text = PlayerPrefs.GetString("gold");

    }
}
