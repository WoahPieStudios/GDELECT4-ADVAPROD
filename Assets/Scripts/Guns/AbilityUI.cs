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




    // Start is called before the first frame update
    private void OnEnable()
    {
        _maxCoolDownTime = (float)onSetCoolDownTime?.Invoke();
        _currentCoolDown = 0;
        Debug.Log($"max cool down set to: {_maxCoolDownTime}");
        _iconFiller.fillAmount = _currentCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        _currentCoolDown = (float)onUpdateCooldown?.Invoke();

        _iconFiller.fillAmount = _currentCoolDown / _maxCoolDownTime;
        if (_iconFiller.fillAmount == 1)
        {
            _iconFiller.color = _readyColor;
        }
        else
        {
            _iconFiller.color = Color.white;
        }
    }

}
