using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const float Delay = 0.5f;

    public Action OnBeginLoading;
    public Action<float> OnLoadProgress;
    public Action<string> OnEndLoading;

    private void Start()
    {
#if !UNITY_EDITOR
        LoadMenu();
#endif
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadScene(SceneNameStorage.MenuName));
    }

    public void LoadLevel(int ind)
    {
        if (ind >= SceneNameStorage.Levels.Length)
        {
            LoadMenu();
            return;
        }


        StartCoroutine(LoadScene(SceneNameStorage.Levels[ind]));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        
        OnBeginLoading?.Invoke();

        AsyncOperation boot = SceneManager.LoadSceneAsync(SceneNameStorage.BootName);

        while (!boot.isDone)
        {
            yield return new WaitForSeconds(Delay);
        }

        AsyncOperation newScene = SceneManager.LoadSceneAsync(sceneName);

        while (!newScene.isDone)
        {
            OnLoadProgress?.Invoke(newScene.progress);
            yield return new WaitForSeconds(Delay);
        }

        OnEndLoading?.Invoke(sceneName);
    }
}