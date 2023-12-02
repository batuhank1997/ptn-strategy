using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class ProductView : EnhancedScrollerCellView
    {
        [SerializeField] private Button m_button;
        [SerializeField] private Image m_image;
        [SerializeField] private TextMeshProUGUI m_tmp;

        public void SetData(ScrollerData scrollerData)
        {
            m_tmp.text = scrollerData.ProductName;
            m_image.sprite = scrollerData.ProductImage;
            m_button.onClick.AddListener(()=> scrollerData.OnClick.Invoke());
        }
    }
}
