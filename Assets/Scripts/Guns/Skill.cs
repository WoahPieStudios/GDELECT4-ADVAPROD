using UnityEngine;
using System.Threading.Tasks;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private float _coolDown;

    [SerializeField]
    private float _equipTime;

    [SerializeField]
    private float _startTimer;
    
    private bool _startCoolDown;
    private float _countDown;
    private bool _canUseSkill;



    public float countDown {
        get => _countDown;
        set => _countDown = Mathf.Clamp(value, 0, _coolDown);
    }


    private void Start()
    {
        countDown = _coolDown;

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
        
    }

    private void ActivateSkill()
    {
        if (!_canUseSkill) return;



    }




}
