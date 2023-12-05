using _Dev.Game.Scripts.Components;
using _Dev.Game.Scripts.Entities.Buildings;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public abstract class Soldier : Unit
    {
        public DamageDealer DamageDealer { get; set; }
        public abstract override ProductData GetProductData();
        
        protected Soldier()
        {
            Mover = new UnitMover();
            DamageDealer = new DamageDealer(((SoldierSo)_productSo).DamageAmount);
        }
    }
}