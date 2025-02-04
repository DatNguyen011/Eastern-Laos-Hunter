using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() => YesNoQuestion(1));
        resumeBtn.onClick.AddListener(() => ResumeGame());
        settingBtn.onClick.AddListener(() => SettingGame());
        exitBtn.onClick.AddListener(() => YesNoQuestion(2));

    }


    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void ResumeGame()
    {
        if (!PlayerPrefs.HasKey("sceneNumber"))
        {
            return;
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("sceneNumber"), LoadSceneMode.Single);

        }
    }
    
    public void SettingGame()
    {

    }
    
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
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
        PlayerPrefs.SetFloat("posX", 0);
        PlayerPrefs.SetFloat("posY", 0);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
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
