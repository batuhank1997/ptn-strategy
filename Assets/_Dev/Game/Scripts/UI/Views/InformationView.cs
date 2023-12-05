using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using _Dev.Game.Scripts.UI.Views.Base;
using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class InformationView : View, IEnhancedScrollerDelegate
    {
        [SerializeField] private EnhancedScroller m_scroller;
        [SerializeField] private InfoProductionViewItem m_informationViewPrefab;

        [SerializeField] private GameObject m_productInfoParent;
        [SerializeField] private GameObject m_productionParent;

        [SerializeField] private Image m_icon;
        [SerializeField] private TextMeshProUGUI m_nameText;
        [SerializeField] private TextMeshProUGUI m_healthText;
        [SerializeField] private TextMeshProUGUI m_amountText;

        private readonly List<ScrollerData> _data = new List<ScrollerData>();

        protected override void OnEnable()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnCellLeftClicked);
            EventSystemManager.AddListener(EventId.on_grid_right_click, OnCellRightClicked);

            ToggleInfoPanel(false);
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
            var cellView = scroller.GetCellView(m_informationViewPrefab) as InfoProductionViewItem;

            cellView.SetData(_data[dataIndex]);

            return cellView;
        }
        
        private void OnCellRightClicked(EventArgs obj)
        {
            ToggleInfoPanel(false);
        }

        protected override void OnDisable()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnCellLeftClicked);
            EventSystemManager.RemoveListener(EventId.on_grid_right_click, OnCellRightClicked);
            base.OnDisable();
        }

        private void OnCellLeftClicked(EventArgs obj)
        {
            ToggleInfoPanel(true);
            
            var args = (Vector2Arguments)obj;
            var cell = GridManager.Instance.GetCell(args.Value);

            _data.Clear();
            m_scroller.Delegate = this;
            m_scroller.ReloadData();

            if (cell.IsOccupied)
                DisplayInfo(cell);
            else
                ToggleInfoPanel(false);
            
            m_scroller.ReloadData();
        }

        private void ToggleInfoPanel(bool isOn)
        {
            m_productInfoParent.SetActive(isOn);
            m_productionParent.SetActive(isOn);
        }

        private void DisplayInfo(Cell cell)
        {
            var cellBuilding = cell.GetBuilding();
            var cellUnits = cell.GetUnits();

            if (cellBuilding != null)
                DisplayBuildingInfo(cellBuilding);
            else if (cellUnits.Count > 0)
                DisplayUnitsInfo(cellUnits);
        }

        private void DisplayBuildingInfo(BoardProduct building)
        {
            var info = building.GetProductData();

            DisplayProductInfo(info);
            m_amountText.text = "";

            if (info.Producer != null)
                CreateProductElements(info.Producer);
        }

        private void DisplayUnitsInfo(List<Unit> units)
        {
            var info = units[0].GetProductData();

            DisplayProductInfo(info);
            m_amountText.text = $"x {units.Count}";
        }

        private void DisplayProductInfo(ProductData info)
        {
            m_icon.sprite = info.Icon;
            m_nameText.text = info.Name;
            m_healthText.text = $"Health: {info.BoardProduct.GetHealth().GetValue()}";
        }

        private void CreateProductElements(IProducer producer)
        {
            foreach (var productData in producer.ProducableProducts)
            {
                SetItemDataAndAdd(productData.BoardProduct, 
                    ()=> producer.Produce(productData.BoardProduct.GetType()));
            }
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