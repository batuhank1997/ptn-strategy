using _Dev.Game.Scripts.Components;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.EventSystem;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Units
{
    public abstract class BoardProduct
    {
        protected ProductSo _productSo;
        protected Health _health;
        
        public abstract ProductData GetProductData();

        protected BoardProduct()
        {
            _health = new Health();
            _productSo = Resources.Load<ProductSo>($"Buildings/{this.GetType().Name}");
        }
        
        public Vector2 GetSize()
        {
            return _productSo.Size;
        }
        
        public Health GetHealth()
        {
            return _health;
        }
        
        public void Die()
        {
            EventSystemManager.InvokeEvent(EventId.on_product_die, new ProductArgs(this));
            Debug.Log($"{this.GetProductData().Name} died");
        }
    }
}