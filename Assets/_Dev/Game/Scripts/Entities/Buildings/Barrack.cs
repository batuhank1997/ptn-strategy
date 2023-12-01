using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.Entities.Units.AttackUnits;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class Barrack : Building, IProducer
    {
        public List<ProductData> ProducableProducts { get; set; }
        public Vector2 SpawnPosition { get; set; }

        private readonly Soldier1 _soldier1 = new();
        private readonly Soldier2 _soldier2 = new();
        private readonly Soldier3 _soldier3 = new();

        public Barrack()
        {
            //todo: refactor this
            _buildingSo = Resources.Load<BuildingSo>("Buildings/Barrack");
            
            ProducableProducts = new List<ProductData>
            {
                _soldier1.GetProductData(),
                _soldier2.GetProductData(),
                _soldier3.GetProductData()
            };
        }
        
        public void Produce()
        {
            //todo: soldier production
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
            return ProducableProducts;
        }
    }
}