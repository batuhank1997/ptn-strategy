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
        private BuildingFactory  _buildingFactory = new BuildingFactory();

        private void Start() 
        {
            _data = new List<ScrollerData>();
            
            var barrack = _buildingFactory.Create<Barrack>();
            var powerPlant = _buildingFactory.Create<PowerPlant>();

            _data.Add(new ScrollerData()
            {
                ProductName = barrack.GetProductData().Name,
                ProductImage = barrack.GetProductData().Icon,
                OnClick = () => PlacingManager.Instance.SetBuildingForPlacing(barrack)
            });
            
            _data.Add(new ScrollerData()
            {
                ProductName = powerPlant.GetProductData().Name,
                ProductImage = powerPlant.GetProductData().Icon,
                OnClick = () => PlacingManager.Instance.SetBuildingForPlacing(powerPlant)
            });

            m_myScroller.Delegate = this;
            m_myScroller.ReloadData();
            
            base.OnEnable();
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
