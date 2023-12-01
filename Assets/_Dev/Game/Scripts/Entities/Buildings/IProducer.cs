using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public interface IProducer
    {
        void Produce();
        public void AddProductToProduction();
        List<ProductData> ProductsInProduction { get; set; }
        Vector2 ProductionSpawnCell { get; }
    }
}
