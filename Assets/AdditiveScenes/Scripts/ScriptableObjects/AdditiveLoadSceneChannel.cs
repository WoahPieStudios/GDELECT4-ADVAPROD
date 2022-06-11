using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Additive Load Scene Channel", menuName = "Channels/Scenes/New Additive Load Scene Channel")]
    public class AdditiveLoadSceneChannel : ScriptableObject
    {
        [SerializeField] private string sceneName;

        public void LoadScene()
        {
            AdditiveSceneManager.OnLoadScene(sceneName);
        }

        public void UnloadScene()
        {
            AdditiveSceneManager.OnUnloadScene(sceneName);
        }
        
    }
}