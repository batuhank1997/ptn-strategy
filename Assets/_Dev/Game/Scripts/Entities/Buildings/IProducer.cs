using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public interface IProducer
    {
        void Produce(Type type);
        List<ProductData> ProducableProducts { get; set; }
        Vector2 SpawnPosition { get; set; }
    }
}
