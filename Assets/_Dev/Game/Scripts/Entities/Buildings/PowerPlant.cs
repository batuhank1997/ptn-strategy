using _Dev.Game.Scripts.Entities.Units;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class PowerPlant : Building
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
