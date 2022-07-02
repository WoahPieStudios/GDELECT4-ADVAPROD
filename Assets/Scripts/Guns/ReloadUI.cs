using UnityEngine;
using UnityEngine.UI;
using System;

public class ReloadUI : MonoBehaviour
{
    [SerializeField]
    private Image _fillBar;


    [SerializeField]
    private GameObject UIFillBar;


    private Gun _gun;
    private bool _doLerp;

    public static event Action onStartFill;
    public static event Action onFinishFill;


    private void Start()
    {
        UIFillBar.SetActive(false);
        
    }

    private void OnEnable()
    {
        _fillBar.fillAmount = 0;
        onStartFill += DoFillBar;
        onFinishFill += StopFillBar;
    }

    private void OnDisable()
    {
        onStartFill -= DoFillBar;
        onFinishFill -= StopFillBar;
    }

    private void Update()
    {
        if (!UIFillBar.activeSelf) return;

      //  UIFillBar.SetActive(true);
        if (_doLerp)
        {
            _fillBar.fillAmount += _gun._reloadSpeed * Time.deltaTime;
        }
    }

    private void DoFillBar()
    {
        UIFillBar.SetActive(true);
        _gun = FindObjectOfType<Gun>();
        _doLerp = true;
    }

    private void StopFillBar()
    {
        _doLerp = false;
        _fillBar.fillAmount = 0;
        UIFillBar.SetActive(false);
    }

    public static void StartFilling() => onStartFill?.Invoke();

    public static void FinishFilling() => onFinishFill?.Invoke();

}
