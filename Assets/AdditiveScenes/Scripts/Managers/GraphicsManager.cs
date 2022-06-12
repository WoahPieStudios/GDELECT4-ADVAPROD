using System;
using AdditiveScenes.Scripts.Enums;
using UnityEngine;

namespace AdditiveScenes.Scripts.Managers
{
    public class GraphicsManager : Singleton<GraphicsManager>
    {
        public GraphicsSettings selectedGraphicsSetting;
        public static event Action onSetLow, onSetMedium, onSetHigh;

        private void Start()
        {
            selectedGraphicsSetting = (GraphicsSettings) QualitySettings.GetQualityLevel();
            print(selectedGraphicsSetting);
        }

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

        public void SetQualityLow ()
        {
            if (QualitySettings.GetQualityLevel() == 0) return;
            selectedGraphicsSetting = GraphicsSettings.Low;
            QualitySettings.SetQualityLevel(0);
            Debug.LogWarning("Graphics set to Low");
        }

        public void SetQualityMedium()
        {
            if (QualitySettings.GetQualityLevel() == 1) return;
            selectedGraphicsSetting = GraphicsSettings.Medium;
            QualitySettings.SetQualityLevel(1);
            Debug.LogWarning("Graphics set to Medium");
        }

        public void SetQualityHigh()
        {
            if (QualitySettings.GetQualityLevel() == 2) return;
            selectedGraphicsSetting = GraphicsSettings.High;
            QualitySettings.SetQualityLevel(2);
            Debug.LogWarning("Graphics set to High");
        }

        public static void OnSetLow()
        {
            onSetLow?.Invoke();
        }
        
        public static void OnSetMedium()
        {
            onSetMedium?.Invoke();
        }
        
        public static void OnSetHigh()
        {
            onSetHigh?.Invoke();
        }
    }
}
