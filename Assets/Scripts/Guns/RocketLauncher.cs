using UnityEngine;

public class RocketLauncher : MonoBehaviour
{

    [SerializeField]
    private Rocket _rocket;

    [SerializeField]
    private Transform _muzzlePoint;

    [SerializeField]
    private float _rocketSpeed;

    [SerializeField]
    private float _blastRadius;

    [SerializeField]
    private float _damage;

    [SerializeField, Tooltip("How long will the rocket last")]
    private float _lifetime = 10f;

    private Rocket _thisRocket;


    private void Awake()
    {
    }

    void Start()
    {
    }

    private void OnEnable()
    {
        Skill.onActivateSkill += Shoot;
    }

    private void OnDisable()
    {
        Skill.onActivateSkill -= Shoot;
    }

    private void Shoot()
    {
        Debug.Log("Rocket Shot");
        Rocket rocket = Instantiate(_rocket) as Rocket;
        rocket.transform.position = _muzzlePoint.position;
        rocket.transform.forward = _muzzlePoint.forward;
        rocket.SetSpeed(_rocketSpeed);
        rocket.SetBlastRadius(_blastRadius);
        rocket.SetDamage(_damage);


        rocket.gameObject.SetActive(true);

        Destroy(rocket, _lifetime);
    }
}
