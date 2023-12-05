using System;
using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class InfoProductionViewItem : EnhancedScrollerCellView
    {
        [SerializeField] private Button m_button;
        [SerializeField] private Image m_image;
        [SerializeField] private TextMeshProUGUI m_nameText;
        [SerializeField] private TextMeshProUGUI m_sizeText;

        private ScrollerData _scrollerData;

        public void SetData(ScrollerData scrollerData)
        {
            _scrollerData = scrollerData;
            m_nameText.text = _scrollerData.ProductSo.Name;
            m_image.sprite = _scrollerData.ProductSo.Icon;

            SetSizeTextIfBigger();
            m_button.onClick.AddListener(OnClick);
        }
        
        public void OnDisable()
        {
            m_button.onClick.RemoveListener(OnClick);
        }

        private void SetSizeTextIfBigger()
        {
            var size = _scrollerData.ProductSo.Size;
            
            if (size.x > 1 || size.y > 1)
                m_sizeText.text = $"{_scrollerData.ProductSo.Size.x}x{_scrollerData.ProductSo.Size.y}";
            else
                m_sizeText.gameObject.SetActive(false);
        }

        private void OnClick()
        {
            _scrollerData.OnClick.Invoke();
        }
    }
}
