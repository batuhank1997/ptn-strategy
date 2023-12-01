using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public interface IProducer
    {
        void Produce();
        public void AddProductToProduction();
        List<ProductData> ProducableProducts { get; set; }
        Vector2 SpawnPosition { get; set; }
    }
}
