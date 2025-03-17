using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMainMenu : MonoBehaviour
{
    [SerializeField] private Button btnOpenMain;
    void Start()
    {
        btnOpenMain.onClick.AddListener(() => Open());
    }

    private void Open()
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
        AsyncLoader.Instance.LoadStart(0);
    }
}
