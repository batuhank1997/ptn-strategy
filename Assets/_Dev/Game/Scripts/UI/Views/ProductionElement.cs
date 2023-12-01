using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.Entities.Units.AttackUnits;
using _Dev.Game.Scripts.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class ProductionElement : MonoBehaviour
    {
        [SerializeField] private Button m_button;
        
        public Image Icon;
        public TextMeshProUGUI Name;
        private ProductData _product;
        
        public void SetElementData(ProductData data)
        {
            _product = data;
            Icon.sprite = _product.Icon;
            Name.text = _product.Name;
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            EventSystemManager.InvokeEvent(EventId.on_production_product_clicked, new TypeArguments(_product.Product.GetType()));
        }
    }
}