using System;
using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class ProductView : EnhancedScrollerCellView
    {
        [SerializeField] private Button m_button;
        [SerializeField] private Image m_image;
        [SerializeField] private TextMeshProUGUI m_tmp;

        private ScrollerData _scrollerData;
        
        public void SetData(ScrollerData scrollerData)
        {
            _scrollerData = scrollerData;
            m_tmp.text = _scrollerData.ProductName;
            m_image.sprite = _scrollerData.ProductImage;
            m_button.onClick.AddListener(delegate { _scrollerData.OnClick.Invoke(); });
        }

        private void OnDestroy()
        {
            m_button.onClick.RemoveListener(delegate { _scrollerData.OnClick.Invoke(); });
        }
    }
}
