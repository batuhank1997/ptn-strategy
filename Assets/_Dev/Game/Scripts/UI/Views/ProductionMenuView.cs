using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.Entities.Units.AttackUnits;
using _Dev.Game.Scripts.Managers;
using _Dev.Game.Scripts.UI.Views.Base;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace _Dev.Game.Scripts.UI.Views
{
    public class ProductionMenuView : View, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller m_myScroller;
        [SerializeField] private ProductView m_productViewPrefab;

        private List<ScrollerData> _data;

        private Barrack _barrack;
        private PowerPlant _powerPlant;
        private Soldier1 _soldier1;
        private Soldier2 _soldier2;
        private Soldier3 _soldier3;

        private void Start()
        {
            CreateItems();
            AddItemsInMenu();

            m_myScroller.Delegate = this;
            m_myScroller.ReloadData();

            base.OnEnable();
        }

        private void OnDestroy()
        {
            _barrack.CleanUp();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _data.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 100f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            var cellView = scroller.GetCellView(m_productViewPrefab) as ProductView;

            cellView.SetData(_data[dataIndex]);

            return cellView;
        }

        private void CreateItems()
        {
            _data = new List<ScrollerData>();

            _barrack = (Barrack)BuildingFactory.Create<Barrack>();
            _powerPlant = (PowerPlant)BuildingFactory.Create<PowerPlant>();

            _soldier1 = (Soldier1)UnitFactory.Create<Soldier1>();
            _soldier2 = (Soldier2)UnitFactory.Create<Soldier2>();
            _soldier3 = (Soldier3)UnitFactory.Create<Soldier3>();
        }

        private void AddItemsInMenu()
        {
            SetItemDataAndAdd(_barrack, () => PlacingManager.Instance.SetBuildingForPlacing(BuildingFactory.Create<Barrack>()));
            SetItemDataAndAdd(_powerPlant, () => PlacingManager.Instance.SetBuildingForPlacing(BuildingFactory.Create<PowerPlant>()));
            SetItemDataAndAdd(_soldier1, () => PlacingManager.Instance.SetUnitForPlacing(UnitFactory.Create<Soldier1>()));
            SetItemDataAndAdd(_soldier2, () => PlacingManager.Instance.SetUnitForPlacing(UnitFactory.Create<Soldier2>()));
            SetItemDataAndAdd(_soldier3, () => PlacingManager.Instance.SetUnitForPlacing(UnitFactory.Create<Soldier3>()));
        }
        
        private void SetItemDataAndAdd(BoardProduct boardProduct, Action onClick)
        {
            _data.Add(new ScrollerData()
            {
                ProductSo = boardProduct.GetSoData(),
                OnClick = onClick
            });
        }
    }
}