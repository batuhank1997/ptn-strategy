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
    }
}