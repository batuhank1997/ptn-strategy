using _Dev.Game.Scripts.Components;
using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public abstract class Building : IProduct
    {
        protected Building()
        {
            Health = new Health();
        }
        
        protected BuildingSo _buildingSo;
        public Vector2 Size => _buildingSo.Size;
        public Health Health { get; set; }
        public abstract ProductData GetProductData();
    }
}
