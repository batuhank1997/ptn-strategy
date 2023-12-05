using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.Entities.Units.AttackUnits;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class Barrack : Building, IProducer
    {
        public List<ProductData> ProducableProducts { get; set; }
    
        public Barrack()
        {
            //todo: refactor this
            EventSystemManager.AddListener(EventId.on_production_product_clicked, OnProducableProductClick);
            
            ProducableProducts = new List<ProductData>
            {
                UnitFactory.Create<Soldier1>().GetProductData(),
                UnitFactory.Create<Soldier2>().GetProductData(),
                UnitFactory.Create<Soldier3>().GetProductData()
            };
        }

        public void Produce(Type type)
        {
            var spawnCell = GridManager.Instance.GetCell(GetSpawnPosition());

            if (!typeof(Unit).IsAssignableFrom(type)) return;
            
            var soldier = (Unit)Activator.CreateInstance(type);
            soldier.SetStartingPosition(spawnCell.GetCoordinates());
            spawnCell.PlaceUnit(soldier);
        }
        
        public Vector2 GetSpawnPosition()
        {
            return _startingPosition + Vector2.left;
        }

        public void SetSpawnableArea()
        {
            var neighbors = GridManager.Instance.GetNeighboringCells(_startingPosition, GetSize());
            
            neighbors.ForEach(n =>
            {
                Debug.Log(n.name);
                n.OccupyForSpawning();
            });
        }

        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = _productSo.Icon,
                Name = _productSo.Name,
                Producer = this,
                BoardProduct = this
            };
        }
               
        public void CleanUp()
        {
            EventSystemManager.RemoveListener(EventId.on_production_product_clicked, OnProducableProductClick);
        }
        
        private void OnProducableProductClick(EventArgs obj)
        {
            var args = (ProductArgs) obj;
            
            if (args.BoardProduct.GetProductData().Producer != this)
                return;

            var type = args.BoardProduct.GetProductData().GetType();
            Produce(type);
        }
   
    }
}