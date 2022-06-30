using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime);
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            ProjectileGun.BulletCollision(this.gameObject);
        }
    }

}
