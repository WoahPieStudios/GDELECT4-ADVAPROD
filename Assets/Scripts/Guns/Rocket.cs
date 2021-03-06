using UnityEngine;
using Handlers;
using AdditiveScenes.Scripts.ScriptableObjects;

public class Rocket : MonoBehaviour
{

    private float _velocity = 5f;
    private float _radius = 1f;
    private float _damage = 0f;
    private float _moveDistance;

    [SerializeField]
    private VFXHandler _vfxHandler;

    [SerializeField]
    private SFXChannel _sfx;
    public void SetSpeed(float newSpeed)
    {
        _velocity = newSpeed;
    }

    public void SetBlastRadius(float newRadius)
    {
        _radius = newRadius;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnEnable()
    {
        Debug.Log($"blast radius: {_radius}");
        Debug.Log($"damage: {_damage}");
        Debug.Log($"bullet speed: {_velocity}");
    }


    void Update()
    {
        _moveDistance = _velocity * Time.deltaTime;
        CollisionCheck(_moveDistance);
        transform.Translate(transform.forward * _velocity * Time.deltaTime,Space.World);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void CollisionCheck(float moveDistance)
    {
        Debug.Log("Explosion");
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 2f, Color.yellow);
        if (Physics.Raycast(ray, out hit, moveDistance))
        {
            OnHitObject(hit.point);
        }
    }

    private void OnHitObject(Vector3 hit)
    {
        Collider[] hitCollisions = Physics.OverlapSphere(hit, _radius);

        foreach(Collider col in hitCollisions)
        {
            IDamageable objectToDamage = col.gameObject.GetComponent<IDamageable>();
            Debug.Log("Giving Damage");
            if ( objectToDamage != null)
            {
                objectToDamage.TakeDamage(_damage);
            }
        }

        _sfx?.PlayAudio();
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Rocket Destroyed");
        _sfx?.PlayAudio();
        OnHitObject(transform.position);
    }
}
