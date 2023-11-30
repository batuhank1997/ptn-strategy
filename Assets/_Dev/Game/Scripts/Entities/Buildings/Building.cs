using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public abstract class Building : IProduct
    {
        protected BuildingSo _buildingSo;
        public Vector2 Size => _buildingSo.Size;
        public string Name => _buildingSo.Name;
    }
}
