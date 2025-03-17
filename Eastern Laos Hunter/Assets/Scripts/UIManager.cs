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
    public GameObject endGamePanel;
    private float elapsedTime = 0;
    public static float deadNumber=0;
    private bool isPaused = false;

    void Start()
    {
        OnInit();
        btnSettings.onClick.AddListener(() => OpenSetting());
        btnMainMenu.onClick.AddListener(() => OpenMainMenu());
        btnClose.onClick.AddListener(() => CloseSetting());
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        PlayerPrefs.SetFloat("time", elapsedTime);
        PlayerPrefs.SetFloat("dead_number", 0);
        PlayerPrefs.Save();
        Pause();
    }

    public void CloseSetting()
    {
        Pause();
    }

    public void OpenEndGame()
    {
        Pause();
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
        Pause();
    }

    public void Pause()
    {
        isPaused = !isPaused;
        settingPanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
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
