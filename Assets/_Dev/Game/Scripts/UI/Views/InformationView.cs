using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using _Dev.Game.Scripts.UI.Views.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class InformationView : View
    {
        [SerializeField] private ProductionElement m_productionElement;
        [SerializeField] private GameObject m_productInfoParent;
        [SerializeField] private GameObject m_productionParent;
        
        [SerializeField] private Image m_icon;
        [SerializeField] private TextMeshProUGUI m_nameText;
        [SerializeField] private TextMeshProUGUI m_healthText;
        [SerializeField] private TextMeshProUGUI m_amountText;
        
        private List<GameObject> _productionElements = new List<GameObject>();
        
        protected override void OnEnable()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnCellClicked);
            EventSystemManager.AddListener(EventId.on_grid_right_click, OnCellRightClicked);
            ToggleInfoPanel(false);
            base.OnEnable();
        }

        private void OnCellRightClicked(EventArgs obj)
        {
            ToggleInfoPanel(false);
        }

        protected override void OnDisable()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnCellClicked);
            EventSystemManager.RemoveListener(EventId.on_grid_right_click, OnCellRightClicked);
            base.OnDisable();
        }

        private void OnCellClicked(EventArgs obj)
        {
            var args = (Vector2Arguments)obj;
            var cell = GridManager.Instance.GetCell(args.Value);

            if (cell.IsOccupied)
            {
                ToggleInfoPanel(true);
                DisplayInfo(cell);
            }
            else
            {
                ToggleInfoPanel(false);
            }
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
        
        private void DisplayBuildingInfo(Building building)
        {
            var info = building.GetProductData();
            
            DisplayProductInfo(info);
            m_amountText.text = "";
            
            foreach (var element in _productionElements)
                Destroy(element);
            
            _productionElements.Clear();

            if (info.Producer != null)
                CreateProductElements(info.Producer);
        }
        
        private void DisplayUnitsInfo(List<Unit> units)
        {
            var info = units[0].GetProductData();
            
            DisplayProductInfo(info);
            m_amountText.text = $"x {units.Count}";
            
            foreach (var element in _productionElements)
                Destroy(element);
            
            _productionElements.Clear();

            if (info.Producer != null)
                CreateProductElements(info.Producer);
        }
        
        private void DisplayProductInfo(ProductData info)
        {
            m_icon.sprite = info.Icon;
            m_nameText.text = info.Name;
            m_healthText.text = $"Health: {info.BoardProduct.GetHealth().GetValue()}";
            
            foreach (var element in _productionElements)
                Destroy(element);
            
            _productionElements.Clear();

            if (info.Producer != null)
                CreateProductElements(info.Producer);
        }
        
        private void CreateProductElements(IProducer producer)
        {
            foreach (var productData in producer.ProducableProducts)
            {
                var element = Instantiate(m_productionElement, m_productionParent.transform);
                element.SetElementData(productData);
                _productionElements.Add(element.gameObject);
            }
        }
    }
}