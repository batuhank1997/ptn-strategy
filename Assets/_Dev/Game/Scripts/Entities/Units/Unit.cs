using System;
using _Dev.Game.Scripts.Components;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Units
{
    public abstract class Unit : IProduct
    {
        protected Unit()
        {
            Health = new Health();
        }
        
        public Health Health { get; set; }
        public UnitMover UnitMover { get; set; }
        public abstract ProductData GetProductData();
        public Type GetProductType() => GetType();
    }
}
