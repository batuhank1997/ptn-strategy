using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier2 : Soldier
    {
        public Soldier2()
        {
            DamageDealer = new DamageDealer(50);
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