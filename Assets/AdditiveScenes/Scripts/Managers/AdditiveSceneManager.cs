using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    [SerializeField] private string[] initialScenes;

    private static event Action<string> onLoadScene; 
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
        onUnloadScene += UnloadScene;
    }

    private void OnDisable()
    {
        onLoadScene -= LoadScene;
        onUnloadScene -= UnloadScene;
    }

    private void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
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

    public static void SetSceneActive(string sceneName)
    {
        var scene = SceneManager.GetSceneByName(sceneName);
        if(scene.isLoaded){
            SceneManager.SetActiveScene(scene);
        }
    }
    
    public static void OnUnloadScene(string sceneName)
    {
        onUnloadScene?.Invoke(sceneName);
    }
}
