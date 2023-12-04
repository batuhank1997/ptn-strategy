using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier3 : Soldier
    {
        public Soldier3()
        {
            DamageDealer = new DamageDealer(10);
        }
        
        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = _productSo.Icon,
                Name = _productSo.Name,
                Producer = null,
                BoardProduct = this
            };
        }
    }
}