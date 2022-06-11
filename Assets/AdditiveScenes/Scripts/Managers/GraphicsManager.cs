using System;
using AdditiveScenes.Scripts.Enums;
using UnityEngine;

namespace AdditiveScenes.Scripts.Managers
{
    public class GraphicsManager : MonoBehaviour
    {
        private static event Action<bool> onSetLow, onSetMedium, onSetHigh;

        private void OnEnable()
        {
            onSetLow += SetQualityLow;
            onSetMedium += SetQualityMedium;
            onSetHigh += SetQualityHigh;
        }

        private void OnDestroy()
        {
            onSetLow -= SetQualityLow;
            onSetMedium -= SetQualityMedium;
            onSetHigh -= SetQualityHigh;
        }

        public void SetQualityLow (bool isLow)
        {
            QualitySettings.SetQualityLevel(0, isLow);
            Debug.LogWarning("Graphics set to Low");
        }

        public void SetQualityMedium(bool isMedium)
        {
            QualitySettings.SetQualityLevel(1, isMedium);
            Debug.LogWarning("Graphics set to Medium");
        }

        public void SetQualityHigh(bool isHigh)
        {
            QualitySettings.SetQualityLevel(2, isHigh);
            Debug.LogWarning("Graphics set to High");
        }

        public static void OnSetLow(bool isLow)
        {
            onSetLow?.Invoke(isLow);
        }
        
        public static void OnSetMedium(bool isMedium)
        {
            onSetMedium?.Invoke(isMedium);
        }
        
        public static void OnSetHigh(bool isHigh)
        {
            onSetHigh?.Invoke(isHigh);
        }
    }
}
