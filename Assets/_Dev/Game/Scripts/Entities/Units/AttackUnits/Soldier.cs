using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public abstract class Soldier : Unit
    {
        protected Soldier()
        {
            Mover = new UnitMover();
        }
        
        public DamageDealer DamageDealer { get; set; }
        public abstract override ProductData GetProductData();
    }
}