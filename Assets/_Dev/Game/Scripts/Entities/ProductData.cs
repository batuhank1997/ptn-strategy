using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities
{
    public class ProductData
    {
        public Sprite Icon;
        public string Name;
        public IProducer Producer;
        public BoardProduct BoardProduct;
    }
}