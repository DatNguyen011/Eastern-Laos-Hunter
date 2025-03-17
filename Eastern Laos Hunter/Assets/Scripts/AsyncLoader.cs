using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : Singleton<AsyncLoader>
{
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider loadingSider;

    private void Start()
    {

    }

    public void LoadStart(int levelNumber)
    {
        loadingScene.SetActive(true);
        StartCoroutine(WaitLoadLevel(levelNumber));
    }


    IEnumerator WaitLoadLevel(int levelNumber)
    {
        loadingSider.value = 0f; 
        float elapsedTime = 0f;
        float loadTime = 2f; 

        while (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            loadingSider.value = Mathf.Clamp01(elapsedTime / loadTime); 
            yield return null;
        }

        loadingSider.value = 1f;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single); 
    }

}
