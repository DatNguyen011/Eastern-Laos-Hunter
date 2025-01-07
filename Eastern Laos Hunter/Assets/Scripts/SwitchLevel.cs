using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    [SerializeField] int screenNumber;
    [SerializeField] Vector2 playerPos;
    [SerializeField] PositionValue posValue;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Hero")
        {
            posValue.initPosValue=playerPos;
            SceneManager.LoadScene(screenNumber,LoadSceneMode.Single);
        }
    }
}
