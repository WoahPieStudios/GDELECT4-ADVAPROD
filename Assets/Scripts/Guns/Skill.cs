using UnityEngine;
using System.Threading.Tasks;
using System;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private float _coolDownTime = 5f;

    [SerializeField]
    private float _equipTime = 0.8f;

    [SerializeField]
    private float _knockBackForce = 100f;


    public static event Action onActivateSkill;

    [SerializeField]
    private GameObject _rocketLauncher;

    private Rigidbody _rb;

    public bool _startCoolDown = false;
    private float _countDown;
    private bool _canUseSkill;



    public float countDown {
        get => _countDown;
        set => _countDown = Mathf.Clamp(value, 0, _coolDownTime);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        countDown = _coolDownTime;
        _rocketLauncher.SetActive(false);
        AbilityUI.onSetCoolDownTime += () =>
        {
           return _coolDownTime;
        };

        AbilityUI.onUpdateCooldown += () =>
        {
            return countDown;
        };
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
        if (_startCoolDown)
        {
            countDown -= Time.deltaTime;

            if (countDown <= 0) 
            {
                _canUseSkill = true;
            }
        }
    }

    private void ActivateSkill()
    {
        if (!_canUseSkill) return;

        _rb.AddForce(-transform.forward * _knockBackForce, ForceMode.Impulse);
        _canUseSkill = false;
        _rocketLauncher.SetActive(true);
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
        _canUseSkill = false;
        await WaitForTimer(_equipTime);
        _rocketLauncher.SetActive(false);
    }



}
