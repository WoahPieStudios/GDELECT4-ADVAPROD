using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace AdditiveScenes.Scripts.Handlers
{
    public class GraphicsToggleHandler : MonoBehaviour
    {
        [SerializeField] private Toggle graphicsToggle;
        [SerializeField] private GraphicsSettingsChannel graphicsSettingsChannel;
        [SerializeField] private SelectedGraphicsSettingsChannel selectedGraphicsSettingsChannel;

        private void OnEnable()
        {
            graphicsToggle.isOn = graphicsSettingsChannel.SelectedGraphicsSettings ==
                                  selectedGraphicsSettingsChannel.selectedGraphicsSettings;
        }
    }
}