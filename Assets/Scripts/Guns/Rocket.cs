using UnityEngine;

public class Rocket : MonoBehaviour
{

    private float _velocity = 5f;
    private float _radius = 1f;
    private float _damage = 0f;
    private float _moveDistance;


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


    void Update()
    {
        _moveDistance = _velocity * Time.deltaTime;
        CollisionCheck(_moveDistance);
        transform.Translate(transform.forward * _velocity * Time.deltaTime,Space.World);
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

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Rocket Destroyed");
        OnHitObject(transform.position);
    }
}
