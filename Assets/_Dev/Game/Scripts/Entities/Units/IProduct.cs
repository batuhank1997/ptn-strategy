using System;
using _Dev.Game.Scripts.Entities.Buildings;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Units
{
    public interface IProduct
    {
        ProductData GetProductData();
    }

    public class ProductData
    {
        public Sprite Icon;
        public string Name;
        public IProducer Producer;
        public IProduct Product;
    }
}