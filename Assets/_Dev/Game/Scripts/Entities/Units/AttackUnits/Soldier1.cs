using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier1 : Soldier
    {
        public Soldier1()
        {
            DamageDealer = new DamageDealer(2);
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