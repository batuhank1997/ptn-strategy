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
        
        public void DealDamage(BoardProduct target)
        {
            target.GetHealth().Damage(_damage);
            
            if (target.GetHealth().GetValue() <= 0)
                target.Die();

            Debug.Log($"target: {target.GetProductData().Name} health: {target.GetHealth().GetValue()}");
        }
    }
}
