using System;
using AdditiveScenes.Scripts.Enums;
using AdditiveScenes.Scripts.Managers;
using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Graphics Setting", menuName = "Channels/Graphics/New Graphics Setting")]
    public class GraphicsSettingsChannel : ScriptableObject
    {
        [SerializeField] private GraphicsSettings selectedGraphicsSettings;

        public void SetGraphics(bool isSelected)
        {
            switch (selectedGraphicsSettings)
            {
                case GraphicsSettings.Low:
                    GraphicsManager.OnSetLow(isSelected);
                    break;
                case GraphicsSettings.Medium:
                    GraphicsManager.OnSetMedium(isSelected);
                    break;
                case GraphicsSettings.High:
                    GraphicsManager.OnSetHigh(isSelected);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}