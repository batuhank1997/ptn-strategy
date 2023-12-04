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
        public Vector2 SpawnPosition { get; set; }
        
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
        
        public void CleanUp()
        {
            EventSystemManager.RemoveListener(EventId.on_production_product_clicked, OnProducableProductClick);
        }
        
        public void OnProducableProductClick(EventArgs obj)
        {
            var args = (TypeArguments) obj;
            var spawnCell = GridManager.Instance.GetCell(SpawnPosition);
            Produce(spawnCell, args.Type);
        }

        public void Produce(Cell spawnCell, Type type)
        {
            if (type == typeof(Soldier1))
            {
                var soldier = UnitFactory.Create<Soldier1>();
                spawnCell.PlaceUnits(new List<Unit> {soldier});
            }
            else if (type == typeof(Soldier2))
            {
                var soldier = UnitFactory.Create<Soldier2>();
                spawnCell.PlaceUnits(new List<Unit> {soldier});
            }
            else if (type == typeof(Soldier3))
            {
                var soldier = UnitFactory.Create<Soldier3>();
                spawnCell.PlaceUnits(new List<Unit> {soldier});
            }
        }
        
        public override ProductData GetProductData()
        {
            Debug.Log($"Product data for {_productSo.Name} requested");
            return new ProductData
            {
                Icon = ImageContainer.Instance.BarracksIcon,
                Name = _productSo.Name,
                Producer = this,
                BoardProduct = this
            };
        }
    }
}