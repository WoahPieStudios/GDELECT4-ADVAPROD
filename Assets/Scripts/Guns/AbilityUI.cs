using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public static event Func<float> onUpdateCooldown;
    public static event Func<float> onSetCoolDownTime;

    [SerializeField]
    private Image _iconFiller;

    [SerializeField]
    private Color _readyColor;

    private float _currentCoolDown;
    private float _maxCoolDownTime;

    private float _currentPercent;

    private void OnEnable()
    {
        _maxCoolDownTime = (float)onSetCoolDownTime?.Invoke();
        _currentCoolDown = 0;
        
    }

    void Update()
    {
         _currentCoolDown = (float)onUpdateCooldown?.Invoke();
        //Debug.LogWarning($"MAX COOL DOWN SET TO: {_maxCoolDownTime}");


        //_currentCoolDown = Mathf.Lerp(0, 1, _maxCoolDownTime);

       // Debug.Log($"current cooldown: {_currentCoolDown / _maxCoolDownTime}");
        _currentPercent = _currentCoolDown / _maxCoolDownTime;
        _iconFiller.fillAmount = _currentPercent;
        if (_currentPercent == 1)
        {
            _iconFiller.color = _readyColor;
        }
        else
        {
            _iconFiller.color = Color.white;
        }
    }

}
