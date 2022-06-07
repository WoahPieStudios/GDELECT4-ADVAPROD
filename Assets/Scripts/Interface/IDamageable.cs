public interface IDamageable
{
    public float Health { get; set; }

    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;
        if(Health <= 0) 
            GetDestroyed();
    }
    public void GetDestroyed();
}
