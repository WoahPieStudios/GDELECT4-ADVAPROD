using UnityEngine;
using System.Threading.Tasks;
using System;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private float _coolDownTime = 5f;

    [SerializeField]
    private float _equipTime = 0.8f;


    public static event Action onActivateSkill;

    [SerializeField]
    private GameObject _rocketLauncher;
    
    public bool _startCoolDown = false;
    private float _countDown;
    private bool _canUseSkill;



    public float countDown {
        get => _countDown;
        set => _countDown = Mathf.Clamp(value, 0, _coolDownTime);
    }


    private void Start()
    {
        countDown = _coolDownTime;
        _rocketLauncher.SetActive(false);
    }


    private void OnEnable()
    {
        InputManager.onSkillActivate += ActivateSkill;
    }

    private void OnDisable()
    {
        InputManager.onSkillActivate -= ActivateSkill;
    }


    private void Update()
    {
        Debug.Log($"countdown timer: {countDown}");
        if (_startCoolDown)
        {
            countDown -= Time.deltaTime;

            if (countDown <= 0) 
            {
                _canUseSkill = true;
            }
        }
    }

    private async void ActivateSkill()
    {
        if (!_canUseSkill) return;
        _canUseSkill = false;

        //do  equipTimeDelay
        await WaitForTimer(_equipTime);
        _rocketLauncher.SetActive(true);
        Debug.Log($"Rocket Set active to: {_rocketLauncher.activeSelf}");
        onActivateSkill?.Invoke();
        ResetTimer();
    }

    private async Task WaitForTimer(float duration)
    {
        var currentTimer = Time.time + _equipTime;

        while (Time.time < currentTimer)
        {
            await Task.Yield();
        }
    }

    private async void ResetTimer()
    {
        countDown = _coolDownTime;

        await WaitForTimer(_equipTime);
        _rocketLauncher.SetActive(false);
    }



}
