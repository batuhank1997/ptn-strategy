using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Buildings;
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
        private Building _powerPlant;
        
        private void Start() 
        {
            _data = new List<ScrollerData>();
            
            var barrack = BuildingFactory.Create<Barrack>();
            var powerPlant = BuildingFactory.Create<PowerPlant>();

            _barrack = (Barrack) barrack;
            _powerPlant = powerPlant;
            
            _data.Add(new ScrollerData()
            {
                ProductName = barrack.GetProductData().Name,
                ProductImage = barrack.GetProductData().Icon,
                OnClick = delegate { PlacingManager.Instance.SetBuildingForPlacing(barrack); }
            });
            
            _data.Add(new ScrollerData()
            {
                ProductName = powerPlant.GetProductData().Name,
                ProductImage = powerPlant.GetProductData().Icon,
                OnClick = delegate { PlacingManager.Instance.SetBuildingForPlacing(powerPlant); }
            });

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
    }
}
