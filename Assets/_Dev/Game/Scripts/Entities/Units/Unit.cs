using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Components;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Units
{
    public abstract class Unit : IProduct
    {
        public Health Health { get; set; }
        public UnitMover Mover { get; set; }
        public abstract ProductData GetProductData();
        
        public Type GetProductType() => GetType();
        
        protected Unit()
        {
            Health = new Health();
        }
    }
}
