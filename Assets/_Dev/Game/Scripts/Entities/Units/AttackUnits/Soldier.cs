using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier : Unit
    {
        protected DamageDealer _damageDealer;
        
        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.SoldierIcon,
                Name = "Soldier"
            };
        }
    }
}