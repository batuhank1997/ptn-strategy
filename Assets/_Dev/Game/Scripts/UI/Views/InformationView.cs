using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using _Dev.Game.Scripts.UI.Views.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class InformationView : View
    {
        [FormerlySerializedAs("m_productionProduct")] [SerializeField] private ProductionElement m_productionElement;
        [SerializeField] private GameObject m_productInfoParent;
        [SerializeField] private GameObject m_productionParent;
        
        [SerializeField] private Image m_icon;
        [SerializeField] private TextMeshProUGUI m_nameText;
        
        private List<GameObject> _productionElements = new List<GameObject>();
        
        protected override void OnEnable()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnCellClicked);
            ToggleInfoPanel(false);
            base.OnEnable();
        }
        
        protected override void OnDisable()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnCellClicked);
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
            var info = cell.GetProduct().GetProductData();
            
            m_icon.sprite = info.Icon;
            m_nameText.text = info.Name;
            
            foreach (var element in _productionElements)
                Destroy(element);
            
            _productionElements.Clear();

            if (info.Producer != null)
                CreateProductElements(info.Producer);
        }
        
        private void CreateProductElements(IProducer producer)
        {
            foreach (var productData in producer.ProductsInProduction)
            {
                var element = Instantiate(m_productionElement, m_productionParent.transform);
                element.SetElementData(productData);
                _productionElements.Add(element.gameObject);
            }
        }
    }
}