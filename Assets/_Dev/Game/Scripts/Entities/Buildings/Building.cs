using System;
using _Dev.Game.Scripts.Components;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public abstract class Building : BoardProduct
    {
        protected Building()
        {
            Health = new Health();
        }
        
        public Health Health { get; set; }
        public void Die()
        {
            EventSystemManager.InvokeEvent(EventId.on_product_die, new ProductArgs(this));
            
            Debug.Log($"{this.GetProductData().Name} died");
        }
    }

    
}
