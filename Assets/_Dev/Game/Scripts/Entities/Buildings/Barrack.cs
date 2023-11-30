using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class Barrack : Building, IProducer
    {
        public List<ProductData> ProductsInProduction { get; set; }

        public Barrack()
        {
            _buildingSo = Resources.Load<BuildingSo>("Buildings/Barrack");
            
            ProductsInProduction = new List<ProductData>
            {
                new ProductData()
                {
                    Icon = ImageContainer.Instance.Soldier1Icon,
                    Name = "Soldier 1",
                },
                new ProductData()
                {
                    Icon = ImageContainer.Instance.Soldier2Icon,
                    Name = "Soldier 2",
                },
                new ProductData()
                {
                    Icon = ImageContainer.Instance.Soldier3Icon,
                    Name = "Soldier 3",
                }
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
