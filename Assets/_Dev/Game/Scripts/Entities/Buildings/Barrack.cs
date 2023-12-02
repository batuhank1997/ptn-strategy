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
            Debug.Log($"constructor {this}");
            _buildingSo = Resources.Load<BuildingSo>("Buildings/Barrack");
            
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
            Debug.Log($"CleanUp {this}");
            EventSystemManager.RemoveListener(EventId.on_production_product_clicked, OnProducableProductClick);
        }
        
        public void OnProducableProductClick(EventArgs obj)
        {
            Debug.Log($"OnProducableProductClick {obj}");
            var args = (TypeArguments) obj;
            var spawnCell = GridManager.Instance.GetCell(SpawnPosition);
            Produce(spawnCell, args.Type);
        }

        public void Produce(Cell spawnCell, Type type)
        {
            if (type == typeof(Soldier1))
            {
                var soldier = UnitFactory.Create<Soldier1>();
                spawnCell.PlaceUnit(soldier);
            }
            else if (type == typeof(Soldier2))
            {
                var soldier = UnitFactory.Create<Soldier2>();
                spawnCell.PlaceUnit(soldier);
            }
            else if (type == typeof(Soldier3))
            {
                var soldier = UnitFactory.Create<Soldier3>();
                spawnCell.PlaceUnit(soldier);
            }
        }
        
        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.BarracksIcon,
                Name = _buildingSo.Name,
                Producer = this,
                Product = this
            };
        }
    }
}