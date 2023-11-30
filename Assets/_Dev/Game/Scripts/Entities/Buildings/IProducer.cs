using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Units;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public interface IProducer
    {
        List<ProductData> ProductsInProduction { get; set; }
        List<ProductData> GetProductsInProduction();
        void Produce();
        public void AddProductToProduction();
    }
}
