using Enums;

namespace Interface
{
    public interface IEnemy : IDamageable, IScoreable
    {
        public EnemyType EnemyType { get; set; }
    }
}