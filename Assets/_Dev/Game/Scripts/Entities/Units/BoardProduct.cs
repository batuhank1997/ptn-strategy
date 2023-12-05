using System.Collections.Generic;
using _Dev.Game.Scripts.Components;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Units
{
    public abstract class BoardProduct
    {
        protected ProductSo _productSo;
        protected Vector2 _startingPosition;
        
        private readonly Health _health;

        public abstract ProductData GetProductData();

        protected BoardProduct()
        {
            _productSo = Resources.Load<ProductSo>($"Products/{this.GetType().Name}");
            _health = new Health(_productSo.HealthLimit);
        }
        
        public ProductSo GetSoData()
        {
            return _productSo;
        }
        
        public Vector2 GetSize()
        {
            return _productSo.Size;
        }

        public List<Cell> GetCells()
        {
            var cells = new List<Cell>();
            
            var xSize = _productSo.Size.x;
            var ySize = _productSo.Size.y;
            
            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    var cell = GridManager.Instance.GetCell(_startingPosition + new Vector2(x, y));
                    cells.Add(cell);
                }
            }

            return cells;
        }
         
        public Health GetHealth()
        {
            return _health;
        }
        
        public void SetStartingPosition(Vector2 position)
        {
            _startingPosition = position;
        }
        
        public void Die()
        {
            EventSystemManager.InvokeEvent(EventId.on_product_die, new ProductArgs(this));
            Debug.Log($"{this.GetProductData().Name} died");
        }
    }
}