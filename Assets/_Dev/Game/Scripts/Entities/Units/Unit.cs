using System;
using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units
{
    public abstract class Unit : IProduct
    {
        protected Health _health;
        protected UnitMover _unitMover;
        public abstract ProductData GetProductData();
        public Type GetProductType() => GetType();
    }
}
