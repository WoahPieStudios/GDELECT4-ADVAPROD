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
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            print($"{scene.name}:{i}");
            if (scene.name == sceneName)
            {
                if(scene.isLoaded){
                    SceneManager.SetActiveScene(scene);
                    break;
                }
                else
                {
                    Debug.LogWarning($"{sceneName} has not yet been loaded.");
                }
            }
            else
            {
                Debug.LogWarning($"{sceneName} has not been found.");
            }
        }
    }
    
    public static void OnUnloadScene(string sceneName)
    {
        onUnloadScene?.Invoke(sceneName);
    }
}
