using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : Singleton<MainMenu>
{
    [SerializeField] Button startBtn;
    [SerializeField] Button resumeBtn;
    [SerializeField] Button settingBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] Button noBtn;
    [SerializeField] Button yesBtn;
    [SerializeField] TMP_Text questionTxt;
    [SerializeField] int sceneNumber;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject yesNoQuestion;
    [SerializeField] GameObject settingPanel;

    void Start()
    {

        startBtn.onClick.AddListener(() => YesNoQuestion(1));
        resumeBtn.onClick.AddListener(() => ResumeGame());
        settingBtn.onClick.AddListener(() => SettingGame());
        exitBtn.onClick.AddListener(() => YesNoQuestion(2));
    }

    public void ResumeGame()
    {
        if (!PlayerPrefs.HasKey("sceneNumber"))
        {
            return;
        }
        else if(PlayerPrefs.HasKey("sceneNumber"))
        {
            AsyncLoader.Instance.LoadStart(PlayerPrefs.GetInt("sceneNumber"));
        }
    }

    public void SettingGame()
    {
        settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;  
    #else
        Application.Quit();  
    #endif
    }

    public void OpenMenu()
    {
        yesNoQuestion.SetActive(false);
        menu.SetActive(true);
    }

    public void StartNewGame()
    {
        PlayerPrefs.SetInt("sceneNumber", 1);
        PlayerPrefs.SetFloat("hp", 100);
        PlayerPrefs.SetFloat("mp", 100);
        PlayerPrefs.SetFloat("posX", -11);
        PlayerPrefs.SetFloat("posY", -4);
        PlayerPrefs.SetFloat("time", 0);
        PlayerPrefs.SetFloat("dead_number", 0);
        PlayerPrefs.SetFloat("maxmp", 100);
        PlayerPrefs.SetFloat("maxhp", 100);
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("scene_" + i, 0);
        }
        PlayerPrefs.Save();
        AsyncLoader.Instance.LoadStart(1);
    }

    public void YesNoQuestion(int a)
    {
        yesNoQuestion.SetActive(true);
        menu.SetActive(false);
        if (a == 1)
        {
            questionTxt.SetText("Do you want to start a new game?");
            yesBtn.onClick.AddListener(() => StartNewGame());
            noBtn.onClick.AddListener(() => OpenMenu());
        }
        else if (a == 2)
        {
            questionTxt.SetText("Do you want quit?");
            yesBtn.onClick.AddListener(() => ExitGame());
            noBtn.onClick.AddListener(() => OpenMenu());
        }
    }


}