using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public abstract class Soldier : Unit
    {
        protected Soldier()
        {
            Mover = new UnitMover();
        }
        
        protected DamageDealer _damageDealer;
        public abstract override ProductData GetProductData();
    }
}