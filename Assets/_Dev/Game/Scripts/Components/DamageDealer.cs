using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Components
{
    public class DamageDealer
    {
        private readonly int _damage;

        public DamageDealer(int damageAmount)
        {
            _damage = damageAmount;
        }
        
        public void DealDamage(IProduct target)
        {
            target.Health.Damage(_damage);
            Debug.Log($"target: {target} health: {target.Health.GetValue()}");
        }
    }
}
