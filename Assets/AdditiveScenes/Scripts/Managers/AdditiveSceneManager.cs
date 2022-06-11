using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    [SerializeField] private string[] initialScenes;

    private static event Action<string> onLoadScene; 
    private static event Action<string,bool> onLoadSceneAdditive;
    private static event Action<string> onUnloadScene; 

    private void Start()
    {
        foreach (var scene in initialScenes)
        {
            LoadScene(scene);
        }
    }

    private void OnEnable()
    {
        onLoadScene += LoadScene;
        onLoadSceneAdditive += LoadSceneAdditive;
        onUnloadScene += UnloadScene;
    }

    private void OnDisable()
    {
        onLoadScene -= LoadScene;
        onLoadSceneAdditive -= LoadSceneAdditive;
        onUnloadScene -= UnloadScene;
    }

    private void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    
    private void LoadSceneAdditive(string sceneName, bool isAdditive)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }

    private void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
    }

    public static void OnLoadScene(string sceneName)
    {
        onLoadScene?.Invoke(sceneName);
    }

    private static void OnOnLoadSceneAdditive(string sceneName, bool isAdditive)
    {
        onLoadSceneAdditive?.Invoke(sceneName, isAdditive);
    }

    public static void OnUnloadScene(string sceneName)
    {
        onUnloadScene?.Invoke(sceneName);
    }
}
