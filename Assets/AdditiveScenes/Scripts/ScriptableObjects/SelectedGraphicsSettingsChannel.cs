using AdditiveScenes.Scripts.Enums;
using AdditiveScenes.Scripts.Managers;
using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Selected Graphics Setting", menuName = "Channels/Graphics/New Selected Graphics Setting")]
    public class SelectedGraphicsSettingsChannel : ScriptableObject
    {
        public GraphicsSettings selectedGraphicsSettings => GraphicsManager.Instance.selectedGraphicsSetting;
    }
}