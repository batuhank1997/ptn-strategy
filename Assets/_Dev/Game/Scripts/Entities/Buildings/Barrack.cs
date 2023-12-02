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
        private UnitFactory _unitFactory = new UnitFactory();
        public List<ProductData> ProducableProducts { get; set; }
        public Vector2 SpawnPosition { get; set; }
        
        public Barrack() : base()
        {
            //todo: refactor this
            _buildingSo = Resources.Load<BuildingSo>("Buildings/Barrack");
            
            EventSystemManager.AddListener(EventId.on_production_product_clicked, OnProducableProductClick);
            
            ProducableProducts = new List<ProductData>
            {
                _unitFactory.Create<Soldier1>().GetProductData(),
                _unitFactory.Create<Soldier2>().GetProductData(),
                _unitFactory.Create<Soldier3>().GetProductData()
            };
        }

        private void OnProducableProductClick(EventArgs obj)
        {
            var args = (TypeArguments) obj;
            var spawnCell = GridManager.Instance.GetCell(SpawnPosition);
            Produce(spawnCell, args.Type);
        }

        public void Produce(Cell spawnCell, Type type)
        {
            var unitFactory = new UnitFactory();
            
            if (type == typeof(Soldier1))
            {
                var soldier = unitFactory.Create<Soldier1>();
                spawnCell.PlaceUnit(soldier);
            }
            else if (type == typeof(Soldier2))
            {
                var soldier = unitFactory.Create<Soldier2>();
                spawnCell.PlaceUnit(soldier);
            }
            else if (type == typeof(Soldier3))
            {
                var soldier = unitFactory.Create<Soldier3>();
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