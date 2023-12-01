using System;
using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units
{
    public abstract class Unit : IProduct
    {
        protected Unit()
        {
            Health = new Health();
        }
        
        protected UnitMover _unitMover;
        public Health Health { get; set; }
        public abstract ProductData GetProductData();
        public Type GetProductType() => GetType();
    }
}
