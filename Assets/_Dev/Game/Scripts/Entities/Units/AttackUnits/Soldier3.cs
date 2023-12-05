namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier3 : Soldier
    {
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