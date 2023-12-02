using System.Collections.Generic;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Managers;
using _Dev.Game.Scripts.UI.Views.Base;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Dev.Game.Scripts.UI.Views
{
    public class ProductionMenuView : View, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller m_myScroller;
        [FormerlySerializedAs("m_productPrefab")] [SerializeField] private ProductView m_productViewPrefab;

        private List<ScrollerData> _data;
        
        void Start () 
        {
            _data = new List<ScrollerData>();

            _data.Add(new ScrollerData()
            {
                ProductName = "Barrack",
                ProductImage = ImageContainer.Instance.BarracksIcon,
                OnClick = () => GridInputManager.Instance.SetBuildingForPlacing(new Barrack())
            });
            _data.Add(new ScrollerData()
            {
                ProductName = "Power Plant",
                ProductImage = ImageContainer.Instance.PowerPlantIcon,
                OnClick = () => GridInputManager.Instance.SetBuildingForPlacing(new PowerPlant())
            });

            m_myScroller.Delegate = this;
            m_myScroller.ReloadData();
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
