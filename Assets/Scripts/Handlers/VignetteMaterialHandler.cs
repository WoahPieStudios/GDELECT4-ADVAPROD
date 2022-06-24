using System;
using UnityEngine;

namespace Handlers
{
    [CreateAssetMenu(menuName = "Handlers/New Vignette Material Handler")]
    public class VignetteMaterialHandler : MaterialHandler
    {
        [Header("Fullscreen Intensity")]
        [SerializeField] private string fullScreenIntensityField;
        [SerializeField, Range(0f, 0.4f)] private float fullScreenIntensityValue;

        public float FullScreenIntensityValue
        {
            get => fullScreenIntensityValue;
            set
            {
                fullScreenIntensityValue = Mathf.Clamp(value, 0f, 0.4f);
                SetMaterialValues();
            }
        }
       
        [Header("Vignette Intensity")]
        [SerializeField] private string vignetteIntensityField;
        [SerializeField, Range(0f, 1f)] private float vignetteIntensityValue;
        public float VignetteIntensityValue
        {
            get => vignetteIntensityValue;
            set
            {
                vignetteIntensityValue = Mathf.Clamp01(value);
                SetMaterialValues();
            }
        }
        
        private void OnValidate()
        {
            SetMaterialValues();
        }

        private void SetMaterialValues()
        {
            material.SetFloat(fullScreenIntensityField, fullScreenIntensityValue);
            material.SetFloat(vignetteIntensityField, vignetteIntensityValue);
        }
    }
}