using UnityEngine;

public class Rocket : MonoBehaviour
{

    private float _velocity = 5f;
    private float _radius = 1f;
    private float _damage = 0f;

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

    private void OnDisable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * _velocity * Time.deltaTime,Space.World);
    }

    private void CollisionCheck(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            OnHitObject(hit);
        }
    }

    private void OnHitObject(RaycastHit hit)
    {
        Collider[] hitCollisions = Physics.OverlapSphere(transform.position, _radius);

        foreach(Collider col in hitCollisions)
        {
            IDamageable objectToDamage = col.gameObject.GetComponent<IDamageable>();
            if ( objectToDamage != null)
            {
                objectToDamage.TakeDamage(_damage);
            }
        }

        Destroy(this.gameObject, 5f);
    }
}
