using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    [SerializeField] private string[] initialScenes;

    private void Start()
    {
        foreach (var scene in initialScenes)
        {
            LoadScene(scene);
        }
    }

    public void LoadScene(string sceneName, bool isAdditive = true)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }
    
}
