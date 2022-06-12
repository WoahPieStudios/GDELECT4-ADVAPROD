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

        public void SetGraphics()
        {
            switch (selectedGraphicsSettings)
            {
                case GraphicsSettings.Low:
                    GraphicsManager.OnSetLow();
                    break;
                case GraphicsSettings.Medium:
                    GraphicsManager.OnSetMedium();
                    break;
                case GraphicsSettings.High:
                    GraphicsManager.OnSetHigh();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}