using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public abstract class Soldier : Unit
    {
        protected DamageDealer _damageDealer;
        
        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.Soldier1Icon,
                Name = "Soldier",
                Producer = null,
                Product = this
            };
        }
    }
}