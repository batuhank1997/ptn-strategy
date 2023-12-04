using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public abstract class ProductSo : ScriptableObject
    {
        public string Name;
        public Vector2 Size;
        public int HealthLimit;
        public Sprite Icon;
    }
}