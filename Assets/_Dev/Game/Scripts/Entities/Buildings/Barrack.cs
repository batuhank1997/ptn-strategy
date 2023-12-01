using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.Entities.Units.AttackUnits;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class Barrack : Building, IProducer
    {
        public List<ProductData> ProductsInProduction { get; set; }
        public Vector2 ProductionPosition { get; }

        private readonly Soldier1 _soldier1 = new();
        private readonly Soldier2 _soldier2 = new();
        private readonly Soldier3 _soldier3 = new();

        public Barrack()
        {
            _buildingSo = Resources.Load<BuildingSo>("Buildings/Barrack");
            ProductionPosition = new Vector2(0, 0);
            //todo: refactor this
            ProductsInProduction = new List<ProductData>
            {
                _soldier1.GetProductData(),
                _soldier2.GetProductData(),
                _soldier3.GetProductData()
            };
        }

        public void Produce()
        {
        }

        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.BarracksIcon,
                Name = _buildingSo.Name,
                Producer = this
            };
        }

        public void AddProductToProduction()
        {
        }

        public List<ProductData> GetProductsInProduction()
        {
            return ProductsInProduction;
        }
    }
}