using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    [CreateAssetMenu(fileName = "ProductSo", menuName = "ScriptableObjects/ProductSo", order = 1)]
    public class ProductSo : ScriptableObject
    {
        public string Name;
        public Vector2 Size;
    }
}