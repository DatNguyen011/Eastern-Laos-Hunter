using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : Singleton<SwitchLevel>
{
    [SerializeField] public int screenNumber=0;
    [SerializeField] Vector2 playerPos;
    [SerializeField] PositionValue posValue;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Hero")
        {
            PlayerPrefs.SetInt("sceneNumber", screenNumber);
            PlayerPrefs.SetFloat("posX", playerPos.x);
            PlayerPrefs.SetFloat("posY", playerPos.y);
            
            posValue.initPosValue=playerPos;
            PlayerPrefs.Save();
            AsyncLoader.Instance.LoadStart(screenNumber);
        }
    }
}
